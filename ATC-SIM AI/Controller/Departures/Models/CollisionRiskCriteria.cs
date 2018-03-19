namespace AtcSimController.Controller.Departures.Models
{
    /// <summary>
    /// Criteria used to determine exact posed risk
    /// </summary>
    enum CollisionRiskCriteria
    {
        CLEARED_ALTITUDE = 0,
        CURRENT_ALTITUDE = 1,
        CLEARED_HEADING = 2,
        PROXIMITY = 3
    }
}
