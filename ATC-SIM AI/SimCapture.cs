using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ATC_SIM_AI
{
    class SimCapture
    {
        private IWebDriver _driver;
        private IJavaScriptExecutor _js;

        /// <summary>
        /// Constructor for the SimCapture class
        /// </summary>
        /// <param name="driver">Current web driver</param>
        public SimCapture(IWebDriver driver)
        {
            this._driver = driver;
            this._js = driver as IJavaScriptExecutor;
        }

        /// <summary>
        /// Returns an array with current simulator statistics
        /// </summary>
        /// <returns>Array of Status (0=Landings, 1=Handoffs, 2=Improper Exits, 3=Missed Approaches, 4=Violation Seconds</returns>
        private int[] CaptureStats()
        {
            int[] stats = new int[5];

            object result = _js.ExecuteScript("return G_arrScore");
            ReadOnlyCollection<object> statsArray = (ReadOnlyCollection<object>)result;
            
            // Cast the objects as ints and place into array
            stats[0] = Convert.ToInt32(statsArray[0]);
            stats[1] = Convert.ToInt32(statsArray[1]);
            stats[2] = Convert.ToInt32(statsArray[2]);
            stats[3] = Convert.ToInt32(statsArray[3]);
            stats[4] = Convert.ToInt32(statsArray[4]);

            return stats;
        }

        /// <summary>
        /// Gets the value of a single JS variable
        /// </summary>
        /// <param name="varName">Name of the variable</param>
        /// <returns>object containing the value</returns>
        public object FetchRawJSVariable(string varName)
        {
            return _js.ExecuteScript("return " + varName);
        }

        /// <summary>
        /// Fetch all flight data from the simulator
        /// </summary>
        /// <returns>Dictionary containing all flights and their data</returns>
        public Dictionary<string, Flight> FetchFlights()
        {
            Dictionary<string, Flight> data = new Dictionary<string, Flight>();

            return data;
        }

        /// <summary>
        /// Gets the statistics for the game
        /// </summary>
        /// <returns>String of stats</returns>
        public string CompileStats()
        {
            // Grab stats
            int[] stats = this.CaptureStats();

            // Compile into string
            string statString =
                "Landings: " + stats[0]
                + "; Handoffs: " + stats[1]
                + "; Improper Exits: " + stats[2]
                + "; Missed Approaches: " + stats[3]
                + "; Violation (secs): " + stats[4];

            return statString;
        }

        /// <summary>
        /// Tells if the simulator is still active
        /// </summary>
        /// <returns>True if active, false if not</returns>
        public bool SimActive()
        {
            return true;
        }
    }
}
