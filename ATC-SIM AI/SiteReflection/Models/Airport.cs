﻿using System.Collections.Generic;

namespace AtcSimController.SiteReflection.Models
{
    /// <summary>
    /// Airport object
    /// </summary>
    public sealed class Airport
    {
        #region Internal accessors
        /// <summary>
        /// Airfield altitude
        /// </summary>
        private int _altitude { get; set; }
        /// <summary>
        /// All airport runways
        /// </summary>
        private List<Waypoint> _runways { get; set; }
        #endregion
        #region Public accessors
        /// <summary>
        /// Airfield altitude
        /// </summary>
        public int Altitude
        {
            get
            {
                return this._altitude;
            }
        }
        /// <summary>
        /// Airport runways
        /// </summary>
        public List<Waypoint> Runways
        {
            get
            {
                return this._runways;
            }
        }
        #endregion

        /// <summary>
        /// Creates a new <see cref="Airport"/> object
        /// </summary>
        /// <param name="altitude">Airfield altitude</param>
        /// <param name="runways">Airfield runway waypoints</param>
        public Airport(int altitude, List<Waypoint> runways)
        {
            this._altitude = altitude;
            this._runways = runways;
        }
    }
}
