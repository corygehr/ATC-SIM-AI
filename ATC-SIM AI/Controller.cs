using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace ATC_SIM_AI
{
    class Controller
    {
        private IWebDriver _driver;
        private Dictionary<string, Flight> _flights;
        private Dictionary<string, Runway> _runways;
        private Dictionary<string, Runway> _activeRwys;

        private int _fieldElev;
        private int _windHdg;

        /// <summary>
        /// Constructor for the Controller class
        /// </summary>
        /// <param name="driver">Current Web Driver object</param>
        public Controller(IWebDriver driver)
        {
            this._driver = driver;
            this._flights = new Dictionary<string, Flight>();
        }

        /// <summary>
        /// Begin the simulator
        /// </summary>
        public void Run()
        {
            // Environment setup
            Console.WriteLine("Setting up controller environment...");

            // Set the 'Scale' command to show edge markers and circles
            new ControllerCommand(this._driver, "SCALE").Execute();

            // Create a capture object
            SimCapture capture = new SimCapture(this._driver);

            // Fetch waypoints, runways
            // TODO

            // Now we begin the main simulator loop
            Console.WriteLine("\n--Controller Active--\n");

            while(capture.SimActive())
            {
                // Fetch new flight data
                this.UpdateFlights(ref capture);

                // Determine active runway(s)
                this.UpdateWind(ref capture);

                // Set routes
                // TODO

                // Output stats
                Console.WriteLine(capture.CompileStats());
            }
        }

        /// <summary>
        /// Updates the active runways based on wind direction
        /// </summary>
        /// <param name="capture">SimCapture object</param>
        private void UpdateActiveRunways(ref SimCapture capture)
        {

        }

        /// <summary>
        /// Updates data for each flight from the simulator
        /// </summary>
        /// <param name="capture">SimCapture object</param>
        private void UpdateFlights(ref SimCapture capture)
        {
            // Load flights from the simulator and update OR add them to our internal flight data
            Dictionary<string, Flight> simFlights = capture.FetchFlights();

            foreach(KeyValuePair<string, Flight> data in simFlights)
            {
                if(this._flights.ContainsKey(data.Key))
                {
                    // Update
                    this._flights[data.Key].Update(data.Value);
                }
                else
                {
                    // Add
                    this._flights.Add(data.Key, data.Value);
                }
            }
            
            // Check for flights that have gone off radar
            foreach(KeyValuePair<string, Flight> data in this._flights)
            {
                // If the flight doesn't exist in what we pulled, the flight is off radar
                if(!simFlights.ContainsKey(data.Key))
                {
                    this._flights.Remove(data.Key);
                }
            }
        }

        /// <summary>
        /// Updates wind speed direction
        /// </summary>
        /// <param name="capture"></param>
        private void UpdateWind(ref SimCapture capture)
        {
            // Get Wind Direction
            this._windHdg = Convert.ToInt32(capture.FetchRawJSVariable("intWind"));
            // Update active runway
            this.UpdateActiveRunways(ref capture);
        }
    }
}
