using AtcSimController.Resources;
using AtcSimController.SiteReflection.Models;
using AtcSimController.SiteReflection.Resources;
using AtcSimController.SiteReflection.SimConnector;

using OpenQA.Selenium;
using System;
using System.Threading;

namespace AtcSimController.Controller
{
    public class Directive
    {
        private Instruction _action;
        private string _command;
        private Boolean _executed = false;
        private Boolean _expedite = false;
        private string _explicitCommand;
        private Flight _flight;
        private string _value;

        /// <summary>
        /// Constructor for the <see cref="Directive"/> class
        /// </summary>
        public Directive()
        {
        }

        /// <summary>
        /// Constructor that allows explicit command specification
        /// </summary>
        /// <param name="command">Full Command Text</param>
        public Directive(string command)
        {
            this._explicitCommand = command;
        }

        /// <summary>
        /// Aborts the current landing or takeoff process for the aircraft
        /// </summary>
        /// <param name="flight">Target flight</param>
        /// <returns>Abort Directive</returns>
        public static Directive Abort(Flight flight)
        {
            return new Directive
            {
                _action = Instruction.ABORT
            };
        }

        /// <summary>
        /// Sets altitude change information for this command
        /// </summary>
        /// <param name="flight">Target flight</param>
        /// <param name="altitude">New altitude</param>
        /// <returns>Altitude Directive</returns>
        /// <remarks>
        /// If altitude provided is in thousands, final directive will reduce it to a 1-2 digit 
        /// number, as this is how the simulator accepts altitude values
        /// </remarks>
        public static Directive ChangeAltitude(Flight flight, int altitude)
        {
            // Convert value to that which would be accepted by the simulator
            if(altitude > 100)
            {
                altitude = altitude / 1000;
            }

            return new Directive
            {
                _action = Instruction.ALTITUDE,
                _flight = flight,
                _value = altitude.ToString()
            };
        }

        /// <summary>
        /// Sets destination change information for this command
        /// </summary>
        /// <param name="flight">Target flight</param>
        /// <param name="destination">New destination</param>
        /// <returns>Destination Directive</returns>
        public static Directive ChangeDestination(Flight flight, Waypoint destination)
        {
            return new Directive
            {
                _action = Instruction.DESTINATION,
                _flight = flight,
                _value = destination.Name
            };
        }

        /// <summary>
        /// Sets speed change information for this command
        /// </summary>
        /// <param name="flight">Target flight</param>
        /// <param name="speed">New speed</param>
        /// <returns>Speed Directive</returns>
        public static Directive ChangeSpeed(Flight flight, int speed)
        {
            return new Directive
            {
                _action = Instruction.SPEED,
                _flight = flight,
                _value = speed.ToString()
            };
        }

        /// <summary>
        /// Compiles a command using the specified parameters
        /// </summary>
        private void CompileCommand()
        {
            if (!String.IsNullOrEmpty(this._explicitCommand))
            {
                // An explicit command was issued
                this._command = this._explicitCommand;
            }
            else
            {
                // Create the command using the values provided
                this._command = String.Format("{0} {1}", _flight.Callsign, _action);

                if(!String.IsNullOrEmpty(_value))
                {
                    this._command = String.Join(" ", this._command, _value);
                }
                
                // Expedite command (if requested)
                if (this._expedite && (_action == Instruction.ALTITUDE || _action == Instruction.SPEED))
                {
                    this._command = String.Join(" ", this._command, Instruction.EXPEDITE);
                }
            }
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute(IWebDriver driver)
        {
            CompileCommand();
            UIHelper.SubmitCommand(driver, this._command);
            // Allow time for the command to execute
            this._executed = true;
        }

        /// <summary>
        /// Sets landing information for this command
        /// </summary>
        /// <param name="flight">Target flight</param>
        /// <param name="runway">Landing Runway Waypoint</param>
        /// <returns>Land Directive</returns>
        public static Directive Land(Flight flight, Waypoint runway)
        {
            return new Directive
            {
                _action = Instruction.LAND,
                _flight = flight,
                _value = runway.Name
            };
        }

        /// <summary>
        /// Sets the target flight for this command
        /// </summary>
        /// <param name="flight">Flight object</param>
        public void SetFlight(Flight flight)
        {
            this._flight = flight;
        }

        /// <summary>
        /// Takeoff instruction
        /// </summary>
        /// <param name="flight">Target flight</param>
        /// <returns>Takeoff Directive</returns>
        public static Directive Takeoff(Flight flight)
        {
            return new Directive
            {
                _action = Instruction.TAKEOFF,
                _flight = flight
            };
        }

        /// <summary>
        /// Returns the text of the command (for debugging only)
        /// </summary>
        public string CommandText
        {
            get
            {
                CompileCommand();
                string output = this._command;

                // Display [EXPLICIT] if the command was explicitly specified
                if (!String.IsNullOrEmpty(this._explicitCommand))
                {
                    output = String.Format(Messages.EXPLICIT_BASE, output);
                }

                return output;
            }
        }

        /// <summary>
        /// Returns true if the command has already been executed
        /// </summary>
        public Boolean Executed
        {
            get
            {
                return this._executed;
            }
        }
    }
}
