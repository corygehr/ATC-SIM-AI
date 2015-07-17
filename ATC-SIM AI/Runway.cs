namespace ATC_SIM_AI
{
    class Runway : Waypoint
    {
        private int _heading;
        private int _length;

        /// <summary>
        /// Constructor for the Runway class
        /// </summary>
        /// <param name="name">Runway name</param>
        /// <param name="location">Location object</param>
        /// <param name="length">Runway length</param>
        /// <param name="heading">Runway heading</param>
        public Runway(string name, Location location, int length, short heading) : base(name, location)
        {
            this._length = length;
            this._heading = heading;
        }

        /// <summary>
        /// Gets the Heading of this runway
        /// </summary>
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
