using AtcSimController.SiteReflection;
using AtcSimController.SiteReflection.SimConnector;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using AtcSimController.Resources;
using AtcSimController.SiteReflection.SimConnector.Resources;

namespace AtcSimController.AIController
{
    class Controller
    {
        private IWebDriver _driver;
        private Dictionary<string, Flight> _flights;
        private List<AircraftModel> _models;

        private SiteReflection.Environment _env;

        private Statistics _stats;

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
            Console.WriteLine(String.Format("{0}...", Messages.ENVIRONMENT_SETUP));

            // Create our statistics object
            this._stats = new Statistics();

            // Set the 'Scale' command to show edge markers and circles
            new ControllerCommand(Instruction.SCALE.ToString()).Execute(this._driver);

            // Create a capture object
            BrowserCapture capture = new BrowserCapture(this._driver);

            // Fetch waypoints, ac models
            this._env = new SiteReflection.Environment(this.FetchWaypoints(ref capture));
            this._models = this.FetchAircraftModels(ref capture);

            // Now we begin the main simulator loop
            Console.WriteLine(String.Format("\n--{0}--\n", Messages.CONTROLLER_READY));

            while(capture.SimActive())
            {
                // Update environment
                this.UpdateEnvironment(ref capture);

                // Fetch new flight data
                this.UpdateFlights(ref capture);

                // Set routes
                // TODO

                // Update stats
                this.RefreshStats(ref capture);
            }
        }

        /// <summary>
        /// Fetches a list of all aircraft models used in the simulation
        /// </summary>
        /// <param name="capture">BrowserCapture object</param>
        /// <returns>List of Aircraft Models</returns>
        private List<AircraftModel> FetchAircraftModels(ref BrowserCapture capture)
        {
            List<AircraftModel> models = new List<AircraftModel>();

            // Get list of models
            JSArray rawModels = new JSArray(capture.FetchRawJSVariable(JSVariables.AIRCRAFT));

            // Parse into a list
            for (int i = 0; i < rawModels.RawData.Count; i++)
            {
                // Parse with JSArray yet again...
                JSArray model = new JSArray(rawModels.RawData[i]);

                // Add to models array
                models.Add(new AircraftModel(
                    Convert.ToInt32(model.RawData[0]),
                    Convert.ToInt32(model.RawData[1]),
                    Convert.ToInt32(model.RawData[2])
                    ));
            }

            return models;
        }

        /// <summary>
        /// Fetch all flight data from the simulator
        /// </summary>
        /// <param name="capture">BrowserCapture object</param>
        /// <returns>Dictionary containing all flights and their data</returns>
        public Dictionary<string, Flight> FetchFlights(ref BrowserCapture capture)
        {
            Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

            // Get list of flights
            //JSArray rawFlts = new JSArray(capture.FetchRawJSVariable("G_objPlanes"));
            Dictionary<string, object> rawFlts = (Dictionary<string, object>)capture.FetchRawJSVariable(JSVariables.FLIGHTS);
            
            // Parse into a list
            foreach(var flt in rawFlts)
            {
                // Get key (flight number)
                string flightNum = flt.Key;

                // Parse out flight data to ReadOnlyCollection
                IDictionary<string, object> data = (IDictionary<string, object>) flt.Value;

                // Convert values to proper types and add new flight to the array
                string aircraft = Convert.ToString(data["0"]);
                int modelInd = Convert.ToInt32(data["1"]);
                int x = Convert.ToInt32(data["2"]);
                int y = Convert.ToInt32(data["3"]);
                int z = Convert.ToInt32(data["4"]);
                int hdg = Convert.ToInt32(data["5"]);
                int spd = Convert.ToInt32(data["6"]);
                int fltMode = Convert.ToInt32(data["7"]);
                int hdgClr = Convert.ToInt32(data["8"]);
                int altClr = Convert.ToInt32(data["9"]);
                int spdClr = Convert.ToInt32(data["10"]);
                string navClr = Convert.ToString(data["11"]);
                int navClrId = Convert.ToInt32(data["12"]);
                int dest = Convert.ToInt32(data["13"]);
                int lr = Convert.ToInt32(data["14"]);
                int timerSec = Convert.ToInt32(data["15"]);
                char timerMode = Convert.ToChar(data["16"]);
                int expedite = Convert.ToInt32(data["17"]);
                bool conflict = Convert.ToBoolean(data["18"]);

                Waypoint destination = this._env.Waypoints[dest];

                Flight newFlt = new Flight(
                    flightNum,
                    this._models[modelInd],
                    (FlightPhase) timerMode,
                    ref destination,
                    z,
                    spd,
                    hdg,
                    x,
                    y
                );

                flights.Add(flightNum, newFlt);
            }
            
            return flights;
        }

        /// <summary>
        /// Fetches a list of all waypoints in the area
        /// </summary>
        /// <param name="capture">BrowserCapture object</param>
        /// <returns>List of Waypoints</returns>
        private List<Waypoint> FetchWaypoints(ref BrowserCapture capture)
        {
            List<Waypoint> wpts = new List<Waypoint>();

            // Get list of waypoints
            JSArray rawWpts = new JSArray(capture.FetchRawJSVariable(JSVariables.WAYPOINTS));

            // Parse into a list
            for(int i=0; i<rawWpts.RawData.Count; i++)
            {
                // Parse with JSArray yet again...
                JSArray wpt = new JSArray(rawWpts.RawData[i]);

                // Grab values
                int heading = -1;

                if(wpt.RawData.Count == 5)
                {
                    // Object is a runway and has an extra index
                    heading = Convert.ToInt32(wpt.RawData[4]);
                }

                // Add to waypoint array
                wpts.Add(new Waypoint(
                    Convert.ToString(wpt.RawData[0]),
                    Convert.ToInt32(wpt.RawData[1]),
                    Convert.ToInt32(wpt.RawData[2]),
                    Convert.ToInt32(wpt.RawData[3]),
                    heading
                    ));
            }

            return wpts;
        }

        /// <summary>
        /// Returns an array with current simulator statistics
        /// </summary>
        /// <param name="capture">BrowserCapture object</param>
        private void RefreshStats(ref BrowserCapture capture)
        {
            JSArray rawStats = new JSArray(capture.FetchRawJSVariable(JSVariables.SCORE));
            this._stats.UpdateStatistics(rawStats.ParseToInt());
        }

        /// <summary>
        /// Updates information about the environment
        /// </summary>
        /// <param name="capture">BrowserCapture object</param>
        private void UpdateEnvironment(ref BrowserCapture capture)
        {
            // Provide wind and active runways
            int wind = Convert.ToInt32(capture.FetchRawJSVariable(JSVariables.WIND_HEADING));
            int[] activeRunways = new JSArray(capture.FetchRawJSVariable(JSVariables.ACTIVE_RUNWAYS)).ParseToInt();

            // Now actually update the environment
            this._env.Update(wind, activeRunways);
        }

        /// <summary>
        /// Updates data for each flight from the simulator
        /// </summary>
        /// <param name="capture">BrowserCapture object</param>
        private void UpdateFlights(ref BrowserCapture capture)
        {
            // Load flights from the simulator and update OR add them to our internal flight data
            Dictionary<string, Flight> simFlights = this.FetchFlights(ref capture);

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
    }
}
