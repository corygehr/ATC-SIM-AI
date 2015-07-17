using System;
using OpenQA.Selenium;

namespace ATC_SIM_AI
{
    class ControllerCommand
    {
        private string _action;
        private IWebDriver _driver;
        private string _command;
        private Boolean _executed = false;
        private Boolean _expedite = false;
        private string _explicitCommand;
        private Flight _flight;
        private string _value;

        /// <summary>
        /// Constructor for the ControllerCommand class
        /// </summary>
        /// <param name="driver">Current Web Driver</param>
        public ControllerCommand(IWebDriver driver)
        {
            this._driver = driver;
        }

        /// <summary>
        /// Constructor that allows explicit command specification
        /// </summary>
        /// <param name="driver">Current Web Driver</param>
        /// <param name="command">Full Command Text</param>
        public ControllerCommand(IWebDriver driver, string command)
        {
            this._driver = driver;
            this._explicitCommand = command;
        }

        /// <summary>
        /// Sets altitude change information for this command
        /// </summary>
        /// <param name="altitude">New altitude</param>
        public void ChangeAltitude(int altitude)
        {
            this._action = SimInstruction.ALTITUDE.ToString();
            this._value = altitude.ToString();
        }

        /// <summary>
        /// Sets destination change information for this command
        /// </summary>
        /// <param name="destination">New destination</param>
        public void ChangeDestination(Waypoint destination)
        {
            this._action = SimInstruction.DESTINATION.ToString();
            this._value = destination.Name;
        }

        /// <summary>
        /// Sets speed change information for this command
        /// </summary>
        /// <param name="speed">New speed</param>
        public void ChangeSpeed(int speed)
        {
            this._action = SimInstruction.SPEED.ToString();
            this._value = speed.ToString();
        }

        /// <summary>
        /// Compiles a command using the specified parameters
        /// </summary>
        private void CompileCommand()
        {
            if (this._explicitCommand != String.Empty)
            {
                // An explicit command was issued
                this._command = this._explicitCommand;
            }
            else
            {
                // Create the command using the values provided

                // If expedite was specified, add the Expedite option at the end
                string expText = "";
                if (this._expedite)
                {
                    expText = "x";
                }

                // Return the created string
                this._command = String.Join(" ", _flight.Name, _action, _value, expText);
            }
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute()
        {
            CompileCommand();
            UIHelper.EnterTextSubmitForm(this._driver, "txtClearance", this._command);
            this._executed = true;
        }

        /// <summary>
        /// Sets landing information for this command
        /// </summary>
        /// <param name="runway">Landing Runway</param>
        public void Land(Runway runway)
        {
            this._action = SimInstruction.LAND.ToString();
            this._value = runway.Name;
        }

        /// <summary>
        /// Sets the target flight for this command
        /// </summary>
        /// <param name="flight">Flight object</param>
        public void SetFlight(ref Flight flight) {
            this._flight = flight;
        }

        /// <summary>
        /// Sets takeoff information for this command
        /// </summary>
        /// <param name="runway">Takeoff Runway</param>
        public void Takeoff(Runway runway) {
            this._action = SimInstruction.TAKEOFF.ToString();
            this._value = runway.Name;
        }

        /// <summary>
        /// Returns the text of the command (for debugging only)
        /// </summary>
        public string CommandText {
            get {
                CompileCommand();
                string output = this._command;

                // Display [EXPLICIT] if the command was explicitly specified
                if (this._explicitCommand != String.Empty)
                {
                    String.Join(" ", "[EXPLICIT]", output);       
                }

                return output;
            }
        }

        /// <summary>
        /// Returns true if the command has already been executed
        /// </summary>
        public Boolean Executed {
            get {
                return this._executed;
            }
        }

    }
}
