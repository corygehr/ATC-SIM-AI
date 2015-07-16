using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_SIM_AI
{
    class Runway : Navpoint
    {
        private int _heading;
        private int _length;

        /// <summary>
        /// Constructor for the Runway class
        /// </summary>
        /// <param name="name">Runway name</param>
        /// <param name="location"></param>
        /// <param name="length"></param>
        /// <param name="heading"></param>
        public Runway(string name, Location location, int length, short heading) : base(name, location)
        {
            this._length = length;
            this._heading = heading;
        }

        public int Heading
        {
            get
            {
                return this._heading;
            }
        }

        /// <summary>
        /// Gets the Length of this Runway
        /// </summary>
        public int Length
        {
            get
            {
                return this._length;
            }
        }
    }
}
