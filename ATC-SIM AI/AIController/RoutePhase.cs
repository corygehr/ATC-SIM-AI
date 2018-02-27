using System;
using System.Collections.Generic;

namespace AtcSimController.AIController
{
    /// <summary>
    /// Route Phase enum
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
        /// Aircraft is aborting their takeoff
        /// </summary>
        public static RoutePhase ABORT_TAKEOFF = new RoutePhase(1, "ABORT_TAKEOFF");
        /// <summary>
        /// Aircraft has been established on the radar scope for arrival
        /// </summary>
        public static RoutePhase ESTABLISHED = new AIController.RoutePhase(2, "ESTABLISHED");
        /// <summary>
        /// Aircraft is executing their departure procedure as provided by the controller
        /// </summary>
        public static RoutePhase DEPARTURE = new RoutePhase(3, "DEPARTURE");
        /// <summary>
        /// Aircraft is going around
        /// </summary>
        public static RoutePhase GO_AROUND = new RoutePhase(4, "GO_AROUND");
        /// <summary>
        /// Aircraft is on the ground after landing
        /// </summary>
        public static RoutePhase GROUND = new RoutePhase(5, "GROUND");
        /// <summary>
        /// Aircraft is landing on the runway
        /// </summary>
        public static RoutePhase LANDING = new RoutePhase(6, "LANDING");
        /// <summary>
        /// Aircraft has left the ground as part of their takeoff procedure
        /// </summary>
        public static RoutePhase LIFTOFF = new RoutePhase(7, "LIFTOFF");
        /// <summary>
        /// Aircraft is holding on the runway for takeoff clearance
        /// </summary>
        public static RoutePhase LINEUP_WAIT = new RoutePhase(8, "LINEUP_WAIT");
        /// <summary>
        /// Aircraft is on approach to the airport (with instructions from the controller)
        /// </summary>
        public static RoutePhase ON_APPROACH = new RoutePhase(9, "ON_APPROACH");
        /// <summary>
        /// Aircraft is on final to the runway
        /// </summary>
        public static RoutePhase ON_FINAL = new RoutePhase(10, "ON_FINAL");
        /// <summary>
        /// Aircraft is ready for takeoff but not on the scope
        /// </summary>
        public static RoutePhase READY_TAKEOFF = new RoutePhase(11, "READY_TAKEOFF");
        /// <summary>
        /// Aircraft is taking off, rolling down the runway
        /// </summary>
        public static RoutePhase ROLLING = new RoutePhase(12, "ROLLING");

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
    }
}
