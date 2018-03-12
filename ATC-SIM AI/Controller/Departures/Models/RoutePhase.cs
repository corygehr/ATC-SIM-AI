using AtcSimController.SiteReflection.Models;
using System;
using System.Collections.Generic;

namespace AtcSimController.Controller.Departures.Models
{
    /// <summary>
    /// Flight Phase enum
    /// </summary>
    public sealed class RoutePhase
    {
        /// <summary>
        /// Enum numeric value
        /// </summary>
        private readonly int _value;
        /// <summary>
        /// Enum string value
        /// </summary>
        private readonly string _name;
        /// <summary>
        /// Enum lookup object
        /// </summary>
        private static Dictionary<string, RoutePhase> _instance = new Dictionary<string, RoutePhase>();
        /// <summary>
        /// Airport data used to determine phase (when required)
        /// </summary>
        public static Airport LocalAirport = null;

        /// <summary>
        /// Aircraft is aborting their takeoff
        /// </summary>
        public static RoutePhase ABORT_TAKEOFF = new RoutePhase(1, "ABORT_TAKEOFF");
        /// <summary>
        /// Aircraft has been established on the radar scope for arrival
        /// </summary>
        public static RoutePhase ESTABLISHED = new RoutePhase(2, "ESTABLISHED");
        /// <summary>
        /// Aircraft is executing their departure procedure as provided by the controller
        /// </summary>
        public static RoutePhase DEPARTURE = new RoutePhase(3, "DEPARTURE");
        /// <summary>
        /// Aircraft is going around
        /// </summary>
        public static RoutePhase GO_AROUND = new RoutePhase(4, "GO_AROUND");
        /// <summary>
        /// Aircraft is landing on the runway
        /// </summary>
        public static RoutePhase LANDING = new RoutePhase(5, "LANDING");
        /// <summary>
        /// Aircraft is being loaded into the simulation
        /// </summary>
        public static RoutePhase LOADING = new RoutePhase(13, "LOADING");
        /// <summary>
        /// Aircraft has left the ground as part of their takeoff procedure
        /// </summary>
        public static RoutePhase LIFTOFF = new RoutePhase(6, "LIFTOFF");
        /// <summary>
        /// Aircraft is holding on the runway for takeoff clearance
        /// </summary>
        public static RoutePhase LINEUP_WAIT = new RoutePhase(7, "LINEUP_WAIT");
        /// <summary>
        /// Aircraft is on approach to the airport (with instructions from the controller)
        /// </summary>
        public static RoutePhase ON_APPROACH = new RoutePhase(8, "ON_APPROACH");
        /// <summary>
        /// Aircraft is on final to the runway
        /// </summary>
        public static RoutePhase ON_FINAL = new RoutePhase(9, "ON_FINAL");
        /// <summary>
        /// Aircraft is ready for takeoff but not on the scope
        /// </summary>
        public static RoutePhase READY_TAKEOFF = new RoutePhase(10, "READY_TAKEOFF");
        /// <summary>
        /// Aircraft is taking off, rolling down the runway
        /// </summary>
        public static RoutePhase ROLLING = new RoutePhase(11, "ROLLING");
        /// <summary>
        /// Aircraft is coming to a stop on the runway
        /// </summary>
        public static RoutePhase STOPPING = new RoutePhase(12, "STOPPING");

        /// <summary>
        /// Creates a new <see cref="RoutePhase"/> enum object
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <param name="name">Enum name</param>
        private RoutePhase(int value, string name)
        {
            this._value = value;
            this._name = name;
            // Add to lookup object
            _instance[name] = this;
        }

        /// <summary>
        /// Returns the string representation of this <see cref="RoutePhase"/>
        /// </summary>
        /// <returns>Enum string value</returns>
        public override string ToString()
        {
            return this._name;
        }

        /// <summary>
        /// Returns true if the provided phase describes an aircraft which is enroute
        /// </summary>
        /// <param name="input">Phase to check</param>
        /// <returns>True if enroute</returns>
        public static bool IsEnroutePhase(RoutePhase input)
        {
            return input == DEPARTURE
                || input == ESTABLISHED;
        }

        /// <summary>
        /// Returns true if the provided phase describes a landing aircraft
        /// </summary>
        /// <param name="input">Phase to check</param>
        /// <returns>True if landing</returns>
        public static bool IsLandingPhase(RoutePhase input)
        {
            return input == LANDING
                || input == ON_APPROACH
                || input == ON_FINAL
                || input == STOPPING;
        }

        /// <summary>
        /// Returns true if the provided phase describes an aircraft which is on the ground or taking off
        /// </summary>
        /// <param name="input">Phase to check</param>
        /// <returns>True if taking off</returns>
        public static bool IsTakeoffPhase(RoutePhase input)
        {
            return input == READY_TAKEOFF
                || input == LINEUP_WAIT
                || input == ROLLING
                || input == LIFTOFF;
        }

        /// <summary>
        /// String to enum value conversion
        /// </summary>
        /// <param name="input">String to process</param>
        public static explicit operator RoutePhase(string input)
        {
            RoutePhase result;
            if(_instance.TryGetValue(input, out result))
            {
                return result;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        /// <summary>
        /// Determines the flight phase
        /// </summary>
        /// <param name="target">Flight to be analyzed</param>
        /// <param name="airport">Airport object</param>
        /// <returns>Current flight phase</returns>
        public static RoutePhase DeterminePhase(Flight target)
        {
            switch(target.Status)
            {
                case Status.DEPARTURE:
                    return READY_TAKEOFF;

                case Status.HOLD:
                    if(target.Altitude == LocalAirport.Altitude)
                    {
                        // Aircraft is still on the ground
                        return LINEUP_WAIT;
                    }
                    else
                    {
                        // Aircraft is holding at a waypoint
                        return DEPARTURE;
                    }

                case Status.TAKEOFF:
                    if(target.Altitude == LocalAirport.Altitude && target.ClearedDestination.Type == WaypointType.RUNWAY)
                    {
                        // Takeoff status with airport altitude is still rolling
                        return ROLLING;
                    }
                    else if(target.Altitude > LocalAirport.Altitude && target.Altitude < (LocalAirport.Altitude + Constants.VERTICAL_SEPARATION_MIN_FT))
                    {
                        // Aircraft must be over 1000ft above field to begin executing turns
                        return LIFTOFF;
                    }
                    else
                    {
                        return DEPARTURE;
                    }

                case Status.ARRIVAL:
                    if(target.ClearedDestination.Type != WaypointType.RUNWAY)
                    {
                        // Aircraft is being routed to the airport
                        return ON_APPROACH;
                    }
                    else if(target.ClearedDestination.Type == WaypointType.RUNWAY && target.Altitude != LocalAirport.Altitude && target.ClearedAltitude == LocalAirport.Altitude)
                    {
                        // Runway as target indicates aircraft is on final
                        return LANDING;
                    }
                    else if(target.ClearedDestination.Type == WaypointType.RUNWAY && target.Altitude == LocalAirport.Altitude && target.ClearedAltitude == LocalAirport.Altitude)
                    {
                        // Aircraft is coming to a stop
                        return STOPPING;
                    }
                    else
                    {
                        return ESTABLISHED;
                    }

                case Status.LOADING:
                    return LOADING;

                default:
                    throw new ArgumentException(String.Format("Unknown Status '{0}' provided. Flight Detail: {1}", target.Status, target.ToString()));
            }
        }
    }
}
