using System;

namespace AtcSimController.SiteReflection.Models
{
    /// <summary>
    /// Aircraft Model Specifications
    /// </summary>
    public sealed class AircraftSpecification
    {
        /// <summary>
        /// Default speed for aircraft when on final (kts)
        /// </summary>
        private int _approachSpeed;
        /// <summary>
        /// Default cruising speed (kts)
        /// </summary>
        private int _cruiseSpeed;
        /// <summary>
        /// Speed aircraft must reach for liftoff (kts)
        /// </summary>
        private int _liftoffSpeed;
        
        /// <summary>
        /// Constructor for the <see cref="AircraftSpecification"/> class, which defines speeds for different aircraft types
        /// </summary>
        /// <param name="cruise">Cruise speed</param>
        /// <param name="liftoff">Liftoff speed</param>
        /// <param name="approach">Approach speed</param>
        public AircraftSpecification(int cruise, int liftoff, int approach)
        {
            this._cruiseSpeed = cruise;
            this._liftoffSpeed = liftoff;
            this._approachSpeed = approach;
        }

        /// <summary>
        /// Gets the aircraft approach speed, which is typically how fast it's going during landings
        /// </summary>
        public int ApproachSpeed
        {
            get
            {
                return this._approachSpeed;
            }
        }

        /// <summary>
        /// Gets the aircraft cruise speed, which is typically the speed it enters our airspace at.
        /// </summary>
        public int CruiseSpeed
        {
            get
            {
                return this._cruiseSpeed;
            }
        }

        /// <summary>
        /// Gets the aircraft Liftoff speed, which is typically the speed it starts to climb at during takeoff
        /// </summary>
        public int LiftoffSpeed
        {
            get
            {
                return this._liftoffSpeed;
            }
        }

        public override string ToString()
        {
            return String.Format("APP:{0};CRU:{1};LFT:{2}",
                this._approachSpeed,
                this._cruiseSpeed,
                this._liftoffSpeed);
        }
    }
}
