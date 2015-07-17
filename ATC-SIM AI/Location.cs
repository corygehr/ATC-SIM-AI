
namespace ATC_SIM_AI
{
    class Location
    {
        private double _x;
        private double _y;

        /// <summary>
        /// Constructor for a Location object
        /// </summary>
        /// <param name="x">X-axis position</param>
        /// <param name="y">Y-axis position</param>
        public Location(double x, double y)
        {
            this._x = x;
            this._y = y;
        }

        public double x
        {
            get { return this._x; }
            set { this._x = value; }
        }

        public double y
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
