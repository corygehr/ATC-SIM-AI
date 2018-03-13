using AtcSimController.Controller.Departures.Models;
using AtcSimController.SiteReflection;
using AtcSimController.SiteReflection.Models;

using System;
using System.Collections.Generic;

namespace AtcSimController.Controller.Departures
{
    /// <summary>
    /// Controller that only handles aircraft takeoffs
    /// </summary>
    public class Departures : TrafficController
    {
        /// <summary>
        /// Flights queued for takeoff
        /// </summary>
        private Queue<Flight> _takeoffQueue = new Queue<Flight>();
        /// <summary>
        /// Aircraft holding on runway(s) for takeoff
        /// </summary>
        private Dictionary<string, string> _runwayReservations = new Dictionary<string, string>();
        /// <summary>
        /// Callsign of flight currently taking off
        /// </summary>
        private string _currentTakeoff = null;
        /// <summary>
        /// Default takeoff altitude
        /// </summary>
        private const int TAKEOFF_ALTITUDE = 7000;
        /// <summary>
        /// Minimum altitude of departing aircraft before another can begin takeoff
        /// </summary>
        private const int TAKEOFF_THRESHOLD = 500;

        /// <summary>
        /// Creates a new <see cref="Departures"/> controller
        /// </summary>
        /// <param name="scope">Radar Scope object</param>
        public Departures(RadarScope scope) : base(scope)
        {
            // Set airport for determining phases
            RoutePhase.LocalAirport = Scope.Airport;

            // Create hold object for aircraft waiting on runways
            foreach(Waypoint runway in scope.Airport.Runways)
            {
                this._runwayReservations[runway.Name] = null;
            }
        }

        public override void DoRouting()
        {
            // TODO
            // Categorize flights by phase to reduce number of enumerations
            /*
            // Add new flights
            this._processNewFlights();
            // Route existing aircraft
            this._routeDepartures();
            // Work through the next Takeoff Queue entry
            this._processTakeoffQueue();*/
            // Do nothing for testing purposes
        }

        /// <summary>
        /// Pushes new flights to the takeoff queue
        /// </summary>
        private void _processNewFlights()
        {
            // Add new flights to the takeoff queue
            foreach (KeyValuePair<string, Flight> entry in Scope.Flights)
            {
                Flight flight = entry.Value;
                if (!this._takeoffQueue.Contains(flight) && RoutePhase.DeterminePhase(flight) == RoutePhase.READY_TAKEOFF)
                {
                    // Add to queue for takeoff runway
                    try
                    {
                        this._takeoffQueue.Enqueue(flight);
                        Console.WriteLine(String.Format("[TOWER] {0} queued for takeoff, Runway {1} (number {2}).", flight.Callsign, flight.ClearedDestination.Name, this._takeoffQueue.Count));
                    }
                    catch (Exception) { }
                }
            }
        }

        /// <summary>
        /// Processes the next takeoff in the queue
        /// </summary>
        private void _processTakeoffQueue()
        {
            // Check if it's safe to takeoff
            if (this._safeToTakeoff())
            {
                // Ensure the current takeoff flight object is empty before proceeding
                this._currentTakeoff = null;
                // Are there flights ready for takeoff?
                if(this._takeoffQueue.Count > 0)
                {
                    // Get next aircraft
                    Flight nextFlight = this._takeoffQueue.Dequeue();
                    this._currentTakeoff = nextFlight.Callsign;
                    // Set takeoff altitude and waypoint
                    Scope.AddDirective(Directive.ChangeAltitude(nextFlight, TAKEOFF_ALTITUDE));
                    Scope.AddDirective(Directive.Takeoff(nextFlight));
                    // Execute directives and hold
                    Scope.ExecuteDirectives();
                    Console.WriteLine(String.Format("[TOWER] {0} cleared for takeoff, Runway {1}.", nextFlight.Callsign, nextFlight.ClearedDestination.Name));
                }
            }

            // Cleanup runway reservations
            foreach(Waypoint runway in Scope.Airport.Runways)
            {
                if(!String.IsNullOrEmpty(this._runwayReservations[runway.Name]))
                {
                    // Aircraft speed determines how far into takeoff the flight could be
                    Flight reserved = Scope.Flights[this._runwayReservations[runway.Name]];

                    if (reserved.Speed > 30)
                    {
                        // Clear reservation spot
                        this._runwayReservations[runway.Name] = null;
                    }
                }
            }

            // Allow position and hold for aircraft queued for takeoff
            foreach(Flight inQueue in this._takeoffQueue)
            {
                // Check if the takeoff runway is already occupied
                if(String.IsNullOrEmpty(this._runwayReservations[inQueue.ClearedDestination.Name]))
                {
                    // Reserve and allow position and hold
                    this._runwayReservations[inQueue.ClearedDestination.Name] = inQueue.Callsign;
                    Scope.AddDirective(Directive.LineupWait(inQueue));
                    Console.WriteLine(String.Format("[TOWER] {0} lineup and wait, Runway {1}.", inQueue.Callsign, inQueue.ClearedDestination.Name));
                    Scope.ExecuteDirectives();
                }
            }
        }

        /// <summary>
        /// Routes aircraft which have already departed
        /// </summary>
        private void _routeDepartures()
        {
            foreach(KeyValuePair<string, Flight> flightEntry in Scope.Flights)
            {
                Flight flight = flightEntry.Value;
                RoutePhase phase = RoutePhase.DeterminePhase(flight);
                if (RoutePhase.IsEnroutePhase(phase))
                {
                    if(flight.Altitude >= (Constants.HANDOFF_ALTITUDE_THRESHOLD_FT + Scope.Airport.Altitude) && phase == RoutePhase.HOLD_WAYPOINT)
                    {
                        // Aircraft meets handoff criteria and is holding - clear to destination
                        Scope.AddDirective(Directive.ChangeDestination(flight, flight.Destination));
                        Scope.ExecuteDirectives();
                        Console.WriteLine(String.Format("[DEPARTURE] {0} cleared to {1}.", flight.Callsign, flight.Destination.Name));
                    }
                    else if(flight.ClearedDestination == null || flight.ClearedDestination.Type == WaypointType.RUNWAY)
                    {
                        // Aircraft has already intercepted destination or is following runway heading
                        Scope.AddDirective(Directive.Hold(flight, flight.Destination));
                        Scope.ExecuteDirectives();
                        Console.WriteLine(String.Format("[DEPARTURE] {0} hold at {1}.", flight.Callsign, flight.Destination.Name));
                    }
                }
            }
        }

        /// <summary>
        /// Determines if it's safe for a new flight to take off
        /// </summary>
        /// <returns>True if ready for next takeoff</returns>
        private bool _safeToTakeoff()
        {
            // Check if there is currently an aircraft taking off
            if (this._currentTakeoff != null)
            {
                // Verify current departing aircraft has reached min safe takeoff threshold
                return Scope.Flights[this._currentTakeoff].Altitude - Scope.Airport.Altitude > TAKEOFF_THRESHOLD;
            }

            return true;
        }
    }
}