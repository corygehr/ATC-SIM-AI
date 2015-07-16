using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC_SIM_AI
{
    class Flight
    {
        private int _altitude;
        private Navpoint _destination;
        private Location _location;
        private string _name;
        private int _speed;
        private int _targetAltitude;

        /// <summary>
        /// Constructor for the Flight class
        /// </summary>
        /// <param name="name">Flight Name</param>
        /// <param name="dest">Destination Navpoint</param>
        /// <param name="speed">Current speed</param>
        /// <param name="location">Current Location</param>
        public Flight(string name, ref Navpoint dest, int speed = 0, Location location = null)
        {
            this._destination = dest;
            this._location = location;
            this._name = name;
            this._speed = speed;
            this._targetAltitude = 0;
        }

        /// <summary>
        /// Constructor for the Flight class that allows creation with explicit X and Y values
        /// </summary>
        /// <param name="name">Flight Name</param>
        /// <param name="dest">Destination Navpoint</param>
        /// <param name="speed">Current speed</param>
        /// <param name="x">Current Location on the X-axis</param>
        /// <param name="y">Current Location on the Y-axis</param>
        public Flight(string name, ref Navpoint dest, int speed = 0, int x = 0, int y = 0)
        {
            this._destination = dest;
            this._location = new Location(x, y);
            this._name = name;
            this._speed = speed;
            this._targetAltitude = 0;
        }

        /// <summary>
        /// Gets the status of this flight
        /// </summary>
        /// <returns>Flight Status (Name, Altitude, Location, Destination, Speed)</returns>
        public string Status()
        {
            return String.Join(" ", this._name, "-", this._altitude, 
                this._location.ToString(), this._destination.Name, this._speed);
        }

        /// <summary>
        /// Updates the information about this flight
        /// </summary>
        /// <param name="dest">Destination Navpoint</param>
        /// <param name="speed">Current speed</param>
        /// <param name="location">Current location</param>
        public void Update(ref Navpoint dest, int speed, Location location)
        {
            this._destination = dest;
            this._location = location;
            this._speed = speed;
        }

        /// <summary>
        /// Returns the altitude of this flight
        /// </summary>
        public int Altitude
        {
            get
            {
                return this._altitude;
            }
        }

        /// <summary>
        /// Returns the Navpoint object for this flight's destination
        /// </summary>
        public Navpoint Destination
        {
            get
            {
                return this._destination;
            }
        }

        /// <summary>
        /// Returns the current location for this flight
        /// </summary>
        public Location Location
        {
            get
            {
                return this._location;
            }
        }

        /// <summary>
        /// Returns the name of this flight
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// Returns the current speed of this flight
        /// </summary>
        public int Speed
        {
            get
            {
                return this._speed;
            }
        }

        /// <summary>
        /// Returns the target altitude of this flight
        /// </summary>
        public int TargetAltitude
        {
            get
            {
                return this._targetAltitude;
            }
        }
    }
}
