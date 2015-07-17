
namespace ATC_SIM_AI
{
    abstract class Waypoint
    {
        private Location _location;
        private string _name;

        /// <summary>
        /// Default constructor for a Waypoint object
        /// </summary>
        /// <param name="name">Waypoint name</param>
        /// <param name="x">Waypoint x location</param>
        /// <param name="y">Waypoint y location</param>
        public Waypoint(string name, double x, double y)
        {
            this._name = name;
            this._location = new Location(x, y);
        }

        /// <summary>
        /// Constructor for a Waypoint object that allows Location object passing
        /// </summary>
        /// <param name="name">Waypoint name</param>
        /// <param name="location">Waypoint Location object</param>
        public Waypoint(string name, Location location)
        {
            this._name = name;
            this._location = location;
        }

        /// <summary>
        /// Returns the Location object for this Navpoint
        /// </summary>
        public Location Location
        {
            get
            {
                return this._location;
            }
        }

        /// <summary>
        /// Returns the name of the Navpoint
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }
    }
}