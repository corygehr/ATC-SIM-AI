using System;

namespace ATC_SIM_AI
{
    class Flight
    {
        private int _altitude;
        private Waypoint _destination;
        private int _heading;
        private Location _location;
        private string _name;
        private int _speed;
        private int _targetAltitude;

        /// <summary>
        /// Constructor for the Flight class
        /// </summary>
        /// <param name="name">Flight Name</param>
        /// <param name="dest">Destination Navpoint</param>
        /// <param name="altitude">Current altitude</param>
        /// <param name="speed">Current speed</param>
        /// <param name="heading">Current heading</param>
        /// <param name="location">Current Location</param>
        public Flight(string name, ref Waypoint dest, int altitude = 0, int speed = 0, int heading = 0, Location location = null)
        {
            this._name = name;
            this._destination = dest;
            this._altitude = altitude;
            this._speed = speed;
            this._heading = heading;
            this._location = location;
            this._targetAltitude = 0;
        }

        /// <summary>
        /// Constructor for the Flight class that allows creation with explicit X and Y values
        /// </summary>
        /// <param name="name">Flight Name</param>
        /// <param name="dest">Destination Navpoint</param>
        /// <param name="altitude">Current altitude</param>
        /// <param name="speed">Current speed</param>
        /// <param name="heading">Current heading</param>
        /// <param name="x">Current Location on the X-axis</param>
        /// <param name="y">Current Location on the Y-axis</param>
        public Flight(string name, ref Waypoint dest, int altitude = 0, int speed = 0, int heading = 0, double x = 0, double y = 0)
        {
            this._name = name;
            this._destination = dest;
            this._altitude = altitude;
            this._speed = speed;
            this._heading = heading;
            this._location = new Location(x, y);
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
        /// <param name="data">Flight object to update from</param>
        public void Update(Flight data)
        {
            if(data.Name == this._name)
            {
                this._destination = data._destination;
                this._altitude = data._altitude;
                this._location = data._location;
                this._speed = data._speed;
            }
            else
            {
                throw new Exception("FATAL: Flight objects do not match.");
            }
        }

        /// <summary>
        /// Updates the information about this flight
        /// </summary>
        /// <param name="dest">Destination Navpoint</param>
        /// <param name="altitude">Current altitude</param>
        /// <param name="speed">Current speed</param>
        /// <param name="location">Current location</param>
        public void Update(ref Waypoint dest, int altitude, int speed, Location location)
        {
            this._destination = dest;
            this._altitude = altitude;
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
        public Waypoint Destination
        {
            get
            {
                return this._destination;
            }
        }

        /// <summary>
        /// Returns the current heading of this flight
        /// </summary>
        public int Heading
        {
            get
            {
                return this._heading;
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
