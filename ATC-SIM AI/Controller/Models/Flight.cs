using System;

namespace AtcSimController.SiteReflection.Models
{
    /// <summary>
    /// Flight object
    /// </summary>
    public sealed class Flight
    {
        private string _airline;
        private int _altitude;
        private int _altitudeTarget;
        private string _callsign;
        private bool _conflict;
        private Waypoint _destination;
        private Waypoint _destinationTarget;
        private bool _expedited;
        private int _heading;
        private int _headingTarget;
        private Location _location;
        private string _model;
        private AircraftSpecification _specs;
        private int _speed;
        private int _speedTarget;
        private int _targetAltitude;
        private Status _type;

        /// <summary>
        /// Constructor for the Flight class
        /// </summary>
        /// <param name="callsign">Flight callsign</param>
        /// <param name="airline">Airline identifier</param>
        /// <param name="model">Aircraft Model Name</param>
        /// <param name="specs">Aircraft Operating Specifications</param>
        /// <param name="type">Flight Type</param>
        /// <param name="dest">Destination Navpoint</param>
        /// <param name="clearedDest">Current destination navpoint</param>
        /// <param name="altitude">Current altitude</param>
        /// <param name="clearedAltitude">Altitude target</param>
        /// <param name="speed">Current speed</param>
        /// <param name="clearedSpeed">Speed target</param>
        /// <param name="heading">Current heading</param>
        /// <param name="clearedHeading">Heading target</param>
        /// <param name="location">Current Location</param>
        /// <param name="conflict">Conflict warning</param>
        /// <param name="expedited">Current instructions are expedited</param>
        public Flight(string callsign, string airline, string model, AircraftSpecification specs, Status type, Waypoint dest, Waypoint clearedDest = null, int altitude = 0, int clearedAltitude = 0, int speed = 0, int clearedSpeed = 0, int heading = 0, int clearedHeading = 0, Location location = null, bool conflict = false, bool expedited = false)
        {
            this._callsign = callsign;
            this._airline = airline;
            this._model = model;
            this._specs = specs;
            this._expedited = expedited;
            this._type = type;
            this._destination = dest;
            this._destinationTarget = clearedDest;
            this._altitude = altitude;
            this._altitudeTarget = clearedAltitude;
            this._speed = speed;
            this._speedTarget = clearedSpeed;
            this._heading = heading;
            this._headingTarget = clearedHeading;
            this._location = location;
            this._conflict = conflict;
            this._targetAltitude = 0;
        }

        /// <summary>
        /// Constructor for the Flight class that allows creation with explicit X and Y values
        /// </summary>
        /// <param name="callsign">Flight callsign</param>
        /// <param name="airline">Airline identifier</param>
        /// <param name="model">Aircraft Model Name</param>
        /// <param name="specs">Aircraft Operating Specifications</param>
        /// <param name="type">Flight Type</param>
        /// <param name="dest">Destination Navpoint</param>
        /// <param name="clearedDest">Current destination navpoint</param>
        /// <param name="altitude">Current altitude</param>
        /// <param name="clearedAltitude">Altitude target</param>
        /// <param name="speed">Current speed</param>
        /// <param name="clearedSpeed">Speed target</param>
        /// <param name="heading">Current heading</param>
        /// <param name="clearedHeading">Heading target</param>
        /// <param name="x">Current Location on the X-axis</param>
        /// <param name="y">Current Location on the Y-axis</param>
        /// <param name="conflict">Conflict warning</param>
        /// <param name="expedited">Current instructions are expedited</param>
        public Flight(string callsign, string airline, string model, AircraftSpecification specs, Status type, Waypoint dest, Waypoint clearedDest = null, int altitude = 0, int clearedAltitude = 0, int speed = 0, int clearedSpeed = 0, int heading = 0, int clearedHeading = 0, double x = 0, double y = 0, bool conflict = false, bool expedited = false)
        {
            this._callsign = callsign;
            this._airline = airline;
            this._model = model;
            this._specs = specs;
            this._expedited = expedited;
            this._type = type;
            this._destination = dest;
            this._destinationTarget = clearedDest;
            this._altitude = altitude;
            this._altitudeTarget = clearedAltitude;
            this._speed = speed;
            this._speedTarget = clearedSpeed;
            this._heading = heading;
            this._headingTarget = clearedHeading;
            this._location = new Location(x, y);
            this._conflict = conflict;
            this._targetAltitude = 0;
        }

        /// <summary>
        /// Gets current details of this flight
        /// </summary>
        /// <returns>Flight Details (Callsign, Altitude, Location, Destination, Speed)</returns>
        public string Details()
        {
            return String.Join(" ", this._callsign, "-", this._altitude, 
                this._location.ToString(), this._destination.Name, this._speed);
        }

        /// <summary>
        /// Updates the information about this flight
        /// </summary>
        /// <param name="data">Flight object to update from</param>
        public void Update(Flight data)
        {
            if(data.Callsign == this._callsign)
            {
                this._destination = data._destination;
                this._altitude = data._altitude;
                this._location = data._location;
                this._speed = data._speed;
                this._expedited = data._expedited;
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
        /// Airline identifier
        /// </summary>
        public string Airline
        {
            get
            {
                return this._airline;
            }
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
        /// Returns the callsign for this flight
        /// </summary>
        public string Callsign
        {
            get
            {
                return this._callsign;
            }
        }

        /// <summary>
        /// Aircraft speed capabilities
        /// </summary>
        public AircraftSpecification Capabilities
        {
            get
            {
                return this._specs;
            }
        }

        /// <summary>
        /// Flight target altitude
        /// </summary>
        public int ClearedAltitude
        {
            get
            {
                return this._altitudeTarget;
            }
        }

        /// <summary>
        /// Current destination <see cref="Waypoint"/>
        /// </summary>
        /// <remarks>
        /// This is the waypoint of the aircraft's runway when it is not in the air
        /// </remarks>
        public Waypoint ClearedDestination
        {
            get
            {
                return this._destinationTarget;
            }
        }

        /// <summary>
        /// Flight target heading
        /// </summary>
        public int ClearedHeading
        {
            get
            {
                return this._headingTarget;
            }
        }

        /// <summary>
        /// Flight target speed
        /// </summary>
        public int ClearedSpeed
        {
            get
            {
                return this._speedTarget;
            }
        }

        /// <summary>
        /// If true, flight is in danger of a collision
        /// </summary>
        public bool ConflictWarning
        {
            get
            {
                return this._conflict;
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
        /// Returns true if the current instructions are to be expedited
        /// </summary>
        public bool IsExpedited
        {
            get
            {
                return this._expedited;
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
        /// Returns true if the flight is visible on the radar scope
        /// </summary>
        public bool OnScope
        {
            get
            {
                return this._location != null;
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

        /// <summary>
        /// Flight Status
        /// </summary>
        public Status Status
        {
            get
            {
                return this._type;
            }
        }

        public override string ToString()
        {
            return this._callsign;
        }
    }
}
