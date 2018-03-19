using AtcSimController.SiteReflection;
using AtcSimController.SiteReflection.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AtcSimController.Controller.Departures.Models
{
    /// <summary>
    /// Conflict detail object
    /// </summary>
    [ExcludeFromCodeCoverage]
    sealed class CollisionRisk
    {
        /// <summary>
        /// Distance between flights
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Risk rating
        /// </summary>
        public CollisionRiskScale Risk { get; set; }
        /// <summary>
        /// Risk factors used to generate risk score
        /// </summary>
        public List<CollisionRiskCriteria> RiskFactors = new List<CollisionRiskCriteria>();
        /// <summary>
        /// Source Flight
        /// </summary>
        public Flight Source { get; set; }
        /// <summary>
        /// Analyzed flight
        /// </summary>
        public Flight Target { get; set; }
        /// <summary>
        /// Altitude between flights
        /// </summary>
        /// <remarks>In relation to source</remarks>
        public int VerticalSeparation { get; set; }

        /// <summary>
        /// Calculates the collision risk for two flights
        /// </summary>
        /// <param name="source">Source flight</param>
        /// <param name="target">Analyzed flight</param>
        /// <returns>Risk assessment</returns>
        public static CollisionRisk CalculateRisk(RadarScope scope, Flight source, Flight target)
        {
            // Get baseline risk
            CollisionRisk risk = new CollisionRisk
            {
                Distance = scope.Distance(source, target),
                Risk = CollisionRiskScale.NO_RISK,
                Source = source,
                Target = target,
                VerticalSeparation = source.Altitude - target.Altitude
            };

            // No risk for flights in landing, waiting, or takeoff phases
            RoutePhase sourcePhase = RoutePhase.DeterminePhase(source);
            RoutePhase targetPhase = RoutePhase.DeterminePhase(target);

            // These categories of phases do not impact collision risk as seen by the simulation
            if(!RoutePhase.IsLandingPhase(sourcePhase) && !RoutePhase.IsLandingPhase(targetPhase) && !RoutePhase.IsTakeoffPhase(sourcePhase) && !RoutePhase.IsTakeoffPhase(targetPhase))
            {
                // Check if current phase indicates risks
                if(source.ConflictWarning)
                {
                    // Check if these two flights are where the risks are present

                    // Check for vertical separation issues
                    if (risk.VerticalSeparation <= Constants.VERTICAL_SEPARATION_MIN_FT)
                    {
                        risk.RiskFactors.Add(CollisionRiskCriteria.CURRENT_ALTITUDE);
                        risk.Risk = CollisionRiskScale.HIGH_RISK;
                    }

                    // Check for proximity issues
                    if (risk.Distance <= Constants.LATERAL_SEPARATION_MIN_PX)
                    {
                        risk.RiskFactors.Add(CollisionRiskCriteria.PROXIMITY);
                        risk.Risk = CollisionRiskScale.HIGH_RISK;
                    }
                }
                else
                {
                    // Check for potential conflict risk

                    // Close vertical separation
                    if (risk.VerticalSeparation <= Constants.VERTICAL_SEPARATION_MIN_FT * 2)
                    {
                        risk.RiskFactors.Add(CollisionRiskCriteria.CLEARED_ALTITUDE);
                        risk.Risk = CollisionRiskScale.LOW_RISK;
                    }
                    
                    // Close lateral separation
                    if (risk.Distance <= Constants.LATERAL_SEPARATION_MIN_PX * 2)
                    {
                        risk.RiskFactors.Add(CollisionRiskCriteria.PROXIMITY);
                        risk.Risk = CollisionRiskScale.LOW_RISK;
                    }
                }
            }

            return risk;
        }
    }
}
