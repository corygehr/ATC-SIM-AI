using System;
using System.Collections.Generic;

namespace AtcSimController.SiteReflection.Models
{
    /// <summary>
    /// Flight object
    /// </summary>
    public class Flight : ScopeObject, IEqualityComparer<Flight>, IEquatable<Flight>
    {
        private string _airline;
        private int _altitude;
        private int _altitudeTarget;
        private string _callsign;
        private bool _conflict;
        private Waypoint _destination;
        private Waypoint _destinationTarget;
        private Direction _direction;
        private bool _expedited;
        private FlightMode _flightMode;
        private int _heading;
        private int _headingTarget;
        private string _model;
        private AircraftSpecification _specs;
        private int _speed;
        private int _speedTarget;
        private Status _type;

        /// <summary>
        /// Constructor for the Flight class that allows creation with explicit X and Y values
        /// </summary>
        /// <param name="callsign">Flight callsign</param>
        /// <param name="airline">Airline identifier</param>
        /// <param name="model">Aircraft Model Name</param>
        /// <param name="specs">Aircraft Operating Specifications</param>
        /// <param name="type">Flight Type</param>
        /// <param name="flightMode">Flight mode</param>
        /// <param name="direction">Turn direction to heading</param>
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
        public Flight(string callsign, string airline, string model, AircraftSpecification specs, Status type, FlightMode flightMode, Direction direction, Waypoint dest, Waypoint clearedDest = null, int altitude = 0, int clearedAltitude = 0, int speed = 0, int clearedSpeed = 0, int heading = 0, int clearedHeading = 0, double x = 0, double y = 0, bool conflict = false, bool expedited = false) : base(x, y)
        {
            this._callsign = callsign;
            this._airline = airline;
            this._model = model;
            this._specs = specs;
            this._expedited = expedited;
            this._type = type;
            this._flightMode = flightMode;
            this._direction = direction;
            this._destination = dest;
            this._destinationTarget = clearedDest;
            this._altitude = altitude;
            this._altitudeTarget = clearedAltitude;
            this._speed = speed;
            this._speedTarget = clearedSpeed;
            this._heading = heading;
            this._headingTarget = clearedHeading;
            this._conflict = conflict;
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
        /// Current Flight State
        /// </summary>
        public FlightMode CurrentState
        {
            get
            {
                return this._flightMode;
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
        /// Flight Status
        /// </summary>
        public Status Status
        {
            get
            {
                return this._type;
            }
        }

        /// <summary>
        /// Returns the target altitude of this flight
        /// </summary>
        public int TargetAltitude
        {
            get
            {
                return this._altitudeTarget;
            }
        }

        /// <summary>
        /// Current turn direction
        /// </summary>
        public Direction TurnDirection
        {
            get
            {
                return this._direction;
            }
        }

        public override string ToString()
        {
            return this._callsign;
        }

        public bool Equals(Flight other)
        {
            return this.Equals(this, other);
        }

        public bool Equals(Flight x, Flight y)
        {
            return x._callsign == y._callsign;
        }

        public int GetHashCode(Flight obj)
        {
            // In this application, flights are equivalent if they have the same callsign - period.
            return obj.Callsign.GetHashCode();
        }
    }
}
