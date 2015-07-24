namespace AtcSimController.SiteReflection.SimConnector
{
    /// <summary>
    /// Contains all possible sim instructions
    /// </summary>
    enum Instruction
    {
        ALTITUDE = 'c',
        DESTINATION = 'c',
        EXPEDITE = 'x',
        LAND = 'l',
        TAKEOFF = 't',
        TURN = 'c',
        SPEED = 's'
    }
}
