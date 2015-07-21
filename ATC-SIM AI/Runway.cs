namespace ATC_SIM_AI
{
    class Runway : Waypoint
    {
        private int _heading;

        /// <summary>
        /// Constructor for the Runway class
        /// </summary>
        /// <param name="name">Runway name</param>
        /// <param name="location">Location object</param>
        /// <param name="heading">Runway heading</param>
        public Runway(string name, Location location, short heading) : base(name, location)
        {
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
    }
}
