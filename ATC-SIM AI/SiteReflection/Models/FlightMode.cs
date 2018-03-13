namespace AtcSimController.SiteReflection.Models
{
    /// <summary>
    /// All supported Flight Modes in ATC-SIM
    /// </summary>
    /// <remarks>
    /// "STACK" means to hold at a vector
    /// </remarks>
    public enum FlightMode
    {
        FREE_FLIGHT = 0,
        QUEUED_TAKEOFF = 1,
        TAKEOFF = 2,
        APPROACH_ROLLOUT = 3,
        INTERCEPT = 4,
        STACK = 5
    }
}
