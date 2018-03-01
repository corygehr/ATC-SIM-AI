using AtcSimController.Resources;
using AtcSimController.SiteReflection.Models;
using AtcSimController.SiteReflection.Resources;
using AtcSimController.SiteReflection.SimConnector;

using OpenQA.Selenium;
using System;

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
        public void Abort()
        {
            this._action = Instruction.ABORT;
        }

        /// <summary>
        /// Sets altitude change information for this command
        /// </summary>
        /// <param name="altitude">New altitude</param>
        public void ChangeAltitude(int altitude)
        {
            this._action = Instruction.ALTITUDE;
            this._value = altitude.ToString();
        }

        /// <summary>
        /// Sets destination change information for this command
        /// </summary>
        /// <param name="destination">New destination</param>
        public void ChangeDestination(Waypoint destination)
        {
            this._action = Instruction.DESTINATION;
            this._value = destination.Name;
        }

        /// <summary>
        /// Sets speed change information for this command
        /// </summary>
        /// <param name="speed">New speed</param>
        public void ChangeSpeed(int speed)
        {
            this._action = Instruction.SPEED;
            this._value = speed.ToString();
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
                this._command = String.Join(" ", _flight.Name, _action, _value);
                
                // Expedite command (if requested)
                if (this._expedite && _action != Instruction.ABORT)
                {
                    this._command = String.Join(" ", this._command, Instruction.EXPEDITE);
                }
                else if(this._expedite && this._action == Instruction.ABORT)
                {
                    Console.WriteLine("[Alert]");
                }
            }
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute(IWebDriver driver)
        {
            CompileCommand();
            UIHelper.EnterTextSubmitForm(driver, FormElements.CLEARANCE_TEXTBOX_ID, this._command);
            this._executed = true;
        }

        /// <summary>
        /// Sets landing information for this command
        /// </summary>
        /// <param name="runway">Landing Runway Waypoint</param>
        public void Land(Waypoint runway)
        {
            this._action = Instruction.LAND;
            this._value = runway.Name;
        }

        /// <summary>
        /// Sets the target flight for this command
        /// </summary>
        /// <param name="flight">Flight object</param>
        public void SetFlight(ref Flight flight)
        {
            this._flight = flight;
        }

        /// <summary>
        /// Sets takeoff information for this command
        /// </summary>
        /// <param name="runway">Takeoff Runway Waypoint</param>
        public void Takeoff(Waypoint runway)
        {
            this._action = Instruction.TAKEOFF;
            this._value = runway.Name;
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
                if (this._explicitCommand != String.Empty)
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
