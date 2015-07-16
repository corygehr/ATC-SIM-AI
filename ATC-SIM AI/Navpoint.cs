using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_SIM_AI
{
    abstract class Navpoint
    {
        private Location _location;
        private string _name;

        /// <summary>
        /// Default constructor for a Navpoint object
        /// </summary>
        /// <param name="name">Navpoint name</param>
        /// <param name="x">Navpoint x location</param>
        /// <param name="y">Navpoint y location</param>
        public Navpoint(string name, int x, int y)
        {
            this._location = new Location(x, y);
            this._name = name;
        }

        /// <summary>
        /// Constructor for a Navpoint object that allows Location object passing
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        public Navpoint(string name, Location location)
        {
            this._location = location;
            this._name = name;
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
