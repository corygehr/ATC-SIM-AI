using System;
using System.Collections.Generic;

namespace AtcSimController.SiteReflection.SimConnector
{
    /// <summary>
    /// Simulator instructions enum
    /// </summary>
    public sealed class Instruction
    {
        /// <summary>
        /// Enum internal value
        /// </summary>
        private readonly int _value;
        /// <summary>
        /// Enum readable name
        /// </summary>
        private readonly string _name;
        /// <summary>
        /// Enum lookup object
        /// </summary>
        private static Dictionary<string, Instruction> _instance = new Dictionary<string, Instruction>();

        /// <summary>
        /// Command designator for aborting a takeoff or landing
        /// </summary>
        public static readonly Instruction ABORT = new Instruction(0, "a");
        /// <summary>
        /// Command designator for changing aircraft altitude
        /// </summary>
        public static readonly Instruction ALTITUDE = new Instruction(1, "c");
        /// <summary>
        /// Command designator for changing aircraft destination
        /// </summary>
        public static readonly Instruction DESTINATION = new Instruction(2, "c");
        /// <summary>
        /// Command designator for expediting aircraft instruction
        /// </summary>
        public static readonly Instruction EXPEDITE = new Instruction(3, "x");
        /// <summary>
        /// Command designator for changing aircraft heading
        /// </summary>
        public static readonly Instruction HEADING = new Instruction(4, "c");
        /// <summary>
        /// Command designator for aircraft landing clearance
        /// </summary>
        public static readonly Instruction LAND = new Instruction(5, "l");
        /// <summary>
        /// Adds edge markers and radar circles to the map
        /// </summary>
        public static readonly Instruction SCALE = new Instruction(6, "SCALE");
        /// <summary>
        /// Command designator for changing aircraft speed
        /// </summary>
        public static readonly Instruction SPEED = new Instruction(6, "s");
        /// <summary>
        /// Command designator for aircraft takeoff clearance
        /// </summary>
        public static readonly Instruction TAKEOFF = new Instruction(7, "t");

        /// <summary>
        /// Creates a new <see cref="Instruction"/> enum
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <param name="name">Enum name</param>
        private Instruction(int value, string name)
        {
            this._value = value;
            this._name = name;
            // Add to lookup object
            _instance[name] = this;
        }

        /// <summary>
        /// Gets the string representation of this enum
        /// </summary>
        /// <returns>Enum string value</returns>
        public override string ToString()
        {
            return this._name;
        }

        /// <summary>
        /// Returns the enum matching a specified string
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>Enum matching provided string</returns>
        public static explicit operator Instruction(string input)
        {
            Instruction result;
            if (_instance.TryGetValue(input, out result))
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
