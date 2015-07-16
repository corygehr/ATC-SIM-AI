using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_SIM_AI
{
    class Location
    {
        private int _x;
        private int _y;

        /// <summary>
        /// Constructor for a Location object
        /// </summary>
        /// <param name="x">X-axis position</param>
        /// <param name="y">Y-axis position</param>
        public Location(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public int x
        {
            get { return this._x; }
            set { this._x = value; }
        }

        public int y
        {
            get { return this._y; }
            set { this._y = value; }
        }

        public override string ToString()
        {
            return ("(" + x + "," + y + ")");
        }
    }
}
