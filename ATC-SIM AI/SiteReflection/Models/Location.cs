using System;
using System.Diagnostics.CodeAnalysis;

namespace AtcSimController.SiteReflection.Models
{
    /// <summary>
    /// Location on the Radar Scope
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Location
    {
        /// <summary>
        /// X-Axis Location (pixel)
        /// </summary>
        private double _x;
        /// <summary>
        /// Y-Axis Location (pixel)
        /// </summary>
        private double _y;

        /// <summary>
        /// Creates a new <see cref="Location"/> object
        /// </summary>
        /// <param name="x">X-axis position (pixels)</param>
        /// <param name="y">Y-axis position (pixels)</param>
        public Location(double x, double y)
        {
            this._x = x;
            this._y = y;
        }

        /// <summary>
        /// Gets the X-coordinate of this <see cref="Location"/> (in pixels)
        /// </summary>
        public double x
        {
            get { return this._x; }
            set { this._x = value; }
        }

        /// <summary>
        /// Gets the Y-coordinate of this <see cref="Location"/> (in pixels)
        /// </summary>
        public double y
        {
            get { return this._y; }
            set { this._y = value; }
        }

        public override string ToString()
        {
            return String.Format("({0},{1})", this._x, this._y);
        }
    }
}
