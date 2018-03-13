namespace AtcSimController.SiteReflection
{
    /// <summary>
    /// Game constants
    /// </summary>
    /// <remarks>
    /// See atc-sim.com for details
    /// </remarks>
    public static class Constants
    {
        /// <summary>
        /// Altitude above field elevation at which a flight is no longer considered "taking off" (in feet)
        /// </summary>
        /// <remarks>
        /// Inferred from game scripts
        /// </remarks>
        public static readonly int FREEFLIGHT_THRESHOLD_FT = 800;
        /// <summary>
        /// Minimum distance between aircraft and navpoint for handoff (in miles)
        /// </summary>
        public static readonly int HANDOFF_MIN_DISTANCE_MI = 1;
        /// <summary>
        /// Minimum altitude above field elevation for aircraft to be handed off to enroute control (in feet)
        /// </summary>
        /// <remarks>
        /// Game instructions state this is 4000 but game script indicates it's Field Elevation + 3500
        /// </remarks>
        public static readonly int HANDOFF_ALTITUDE_THRESHOLD_FT = 3500;
        /// <summary>
        /// Maximum difference between aircraft and ground level altitudes for landing clearance (in feet)
        /// </summary>
        public static readonly int LANDING_ALTITUDE_THRESHOLD_FT = 3000;
        /// <summary>
        /// Maximum difference between aircraft and runway heading for landing clearance (in degrees)
        /// </summary>
        public static readonly int LANDING_HEADING_THRESHOLD_DEG = 60;
        /// <summary>
        /// Minimum lateral separation of aircraft (miles)
        /// </summary>
        public static readonly int LATERAL_SEPARATION_MIN_MI = 3;
        /// <summary>
        /// Minimum vertical separation of aircraft (feet)
        /// </summary>
        public static readonly int VERTICAL_SEPARATION_MIN_FT = 1000;
    }
}
