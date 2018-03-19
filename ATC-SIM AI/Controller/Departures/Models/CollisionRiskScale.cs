namespace AtcSimController.Controller.Departures.Models
{
    /// <summary>
    /// Collision Risk scale
    /// </summary>
    enum CollisionRiskScale
    {
        /// <summary>
        /// No conflict present
        /// </summary>
        NO_RISK = 0,
        /// <summary>
        /// Current risk exists but should clear assuming no changes in clearance
        /// </summary>
        LOW_RISK = 1,
        /// <summary>
        /// Risks are likely to be realized based on clearances, assuming no change
        /// </summary>
        MED_RISK = 2,
        /// <summary>
        /// Current conflict is present in the simulation
        /// </summary>
        HIGH_RISK = 3
    }
}
