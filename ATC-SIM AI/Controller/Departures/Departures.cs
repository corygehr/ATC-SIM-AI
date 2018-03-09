using AtcSimController.Controller.Departures.Models;
using AtcSimController.SiteReflection.Models;
using System;
using System.Collections.Generic;

namespace AtcSimController.Controller.Departures
{
    public class Departures : TrafficController
    {
        /// <summary>
        /// Flights queued for takeoff
        /// </summary>
        private Queue<Flight> _takeoffQueue = new Queue<Flight>();
        /// <summary>
        /// Current flight that's taking off
        /// </summary>
        private string _currentTakeoff;
        /// <summary>
        /// Minimum altitude separation
        /// </summary>
        private const int SAFE_ALTITUDE = 1000;
        /// <summary>
        /// Default takeoff altitude
        /// </summary>
        private const int TAKEOFF_ALTITUDE = 7000;

        /// <summary>
        /// Creates a new <see cref="Departures"/> controller
        /// </summary>
        /// <param name="scope">Radar Scope object</param>
        public Departures(RadarScope scope) : base(scope)
        {
        }

        public override void DoRouting()
        {
            // Categorize flights
            this._processFlights();
            // Work through the next Takeoff Queue entry
            this._processTakeoffQueue();
        }

        /// <summary>
        /// Pushes new flights to the takeoff queue
        /// </summary>
        private void _processFlights()
        {
            // Add new flights to the takeoff queue
            foreach (KeyValuePair<string, Flight> entry in Scope.Flights)
            {
                Flight flight = entry.Value;
                if (!this._takeoffQueue.Contains(flight) && RoutePhase.DeterminePhase(flight, Scope.Airport) == RoutePhase.READY_TAKEOFF)
                {
                    // Add to queue for takeoff runway
                    try
                    {
                        this._takeoffQueue.Enqueue(flight);
                        Console.WriteLine(String.Format("[VERBOSE] {0} queued for takeoff (current number {1}).", flight.Callsign, this._takeoffQueue.Count));
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
                    Scope.AddDirective(Directive.ChangeDestination(nextFlight, nextFlight.Destination));
                    Scope.AddDirective(Directive.Takeoff(nextFlight));
                    // Execute directives and hold
                    Scope.ExecuteDirectives();
                    Console.WriteLine(String.Format("[VERBOSE] {0} cleared for takeoff.", nextFlight.Callsign));
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
                // If the flight is over 1000 above the airport, it's safe for the next flight to takeoff
                return Scope.Flights[this._currentTakeoff].Altitude - Scope.Airport.Altitude > SAFE_ALTITUDE;
            }

            return true;
        }
    }
}