using System.Diagnostics.CodeAnalysis;

namespace AtcSimController.SiteReflection.Models
{
    /// <summary>
    /// Parent class for any object that can appear on the Radar Scope
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class ScopeObject
    {
        protected Location _location;

        /// <summary>
        /// Creates a new <see cref="ScopeObject"/> object
        /// </summary>
        /// <param name="location">Object location on the scope</param>
        public ScopeObject(Location location)
        {
            this._location = location;
        }

        /// <summary>
        /// Creates a new <see cref="ScopeObject"/> object using specific coordinates
        /// </summary>
        /// <param name="x">X-coordinate of object</param>
        /// <param name="y">Y-coordinate of object</param>
        public ScopeObject(double x, double y)
        {
            this._location = new Location(x, y);
        }

        /// <summary>
        /// Current Object Location
        /// </summary>
        public Location Location
        {
            get
            {
                return this._location;
            }
        }
    }
}
