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
            // Categorize flights by phase to reduce number of enumerations
            // Group by control
            List<Flight> ground = new List<Flight>();
            List<Flight> departure = new List<Flight>();
            List<Flight> tower = new List<Flight>();

            foreach(KeyValuePair<string, Flight> flight in Scope.Flights)
            {
                RoutePhase flightPhase = RoutePhase.DeterminePhase(flight.Value);
                if (RoutePhase.IsEnroutePhase(flightPhase))
                {
                    departure.Add(flight.Value);
                }
                else if (!this._takeoffQueue.Contains(flight.Value) && flightPhase == RoutePhase.READY_TAKEOFF)
                {
                    ground.Add(flight.Value);
                }
                else
                {
                    tower.Add(flight.Value);
                }
            }

            // Queue flights for takeoff
            this._processNewFlights(ground);
            // Route existing aircraft
            this._routeDepartures(departure);
            // Process takeoff queue
            this._processTakeoffQueue();
        }

        /// <summary>
        /// Pushes new flights to the takeoff queue
        /// </summary>
        /// <param name="flights">Flights to queue</param>
        private void _processNewFlights(List<Flight> flights)
        {
            // Add new flights to the takeoff queue
            foreach (Flight flight in flights)
            {
                // Add to queue for takeoff runway
                this._takeoffQueue.Enqueue(flight);
                Console.WriteLine(String.Format("[TOWER] {0} queued for takeoff, Runway {1} (number {2}).", flight.Callsign, flight.ClearedDestination.Name, this._takeoffQueue.Count));
            }
        }

        /// <summary>
        /// Processes the takeoff queue
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
        /// <param name="flights">Enroute Flights</param>
        private void _routeDepartures(List<Flight> flights)
        {
            foreach(Flight flight in flights)
            {
                RoutePhase phase = RoutePhase.DeterminePhase(flight);
                if (RoutePhase.IsEnroutePhase(phase))
                {
                    // Ensure flight is currently enroute to destination
                    if(flight.ClearedDestination != flight.Destination)
                    {
                        Scope.AddDirective(Directive.ChangeDestination(flight, flight.Destination));
                        Scope.ExecuteDirectives();
                        Console.WriteLine(String.Format("[DEPARTURE] {0} cleared to {1}.", flight.Callsign, flight.Destination.Name));
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