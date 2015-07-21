using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_SIM_AI
{
    class AircraftModel
    {
        private int _cruiseSpeed;
        private int _liftoffSpeed;
        private int _approachSpeed;
        
        /// <summary>
        /// Constructor for the AircraftModel class, which defines speeds for different aircraft types
        /// </summary>
        /// <param name="cruise">Cruise speed</param>
        /// <param name="liftoff">Liftoff speed</param>
        /// <param name="approach">Approach speed</param>
        public AircraftModel(int cruise, int liftoff, int approach)
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
    }
}
