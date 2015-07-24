namespace AtcSimController.AIController
{
    enum RoutePhase
    {
        ARRSCOPE, // Arrival on scope
        APPROACH, // On approach
        FINAL,    // On final
        LANDING,  // Landing
        GROUND,   // On ground
        HOLD,     // Position and hold
        READYTO,  // Ready for takeoff (not on scope)
        ROLLING,  // Taking off, on ground
        LIFTOFF,  // Taking off, off ground
        DEPARTURE // Executing SID
    }
}
