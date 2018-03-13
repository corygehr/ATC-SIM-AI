using System.Diagnostics.CodeAnalysis;

namespace AtcSimController.SiteReflection.Models
{
    /// <summary>
    /// Airway object (entry point for aircraft)
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Airway
    {
        /// <summary>
        /// Base aircraft heading
        /// </summary>
        private int _baseHeading;
        /// <summary>
        /// Airway location
        /// </summary>
        private Location _location;

        /// <summary>
        /// Base Heading used for aircraft entering via this heading
        /// </summary>
        /// <remarks>
        /// Varies +-30degrees per entering aircraft
        /// </remarks>
        public int BaseHeading
        {
            get
            {
                return this._baseHeading;
            }
        }

        /// <summary>
        /// Airway Location
        /// </summary>
        public Location Location
        {
            get
            {
                return this._location;
            }
        }

        /// <summary>
        /// Creates a new <see cref="Airway"/> object
        /// </summary>
        /// <param name="location">Radar Scope location</param>
        /// <param name="baseHeading">Default heading used by aircraft entering scope via this <see cref="Airway"/></param>
        public Airway(Location location, int baseHeading)
        {
            this._baseHeading = baseHeading;
            this._location = location;
        }

        /// <summary>
        /// Creates a new <see cref="Airway"/> object
        /// </summary>
        /// <param name="x">Radar Scope X-location</param>
        /// <param name="y">Radar Scope Y-location</param>
        /// <param name="baseHeading">Default heading used by aircraft entering scope via this <see cref="Airway"/></param>
        public Airway(int x, int y, int baseHeading)
        {
            this._baseHeading = baseHeading;
            this._location = new Location(x, y);
        }
    }
}
