using System.Collections.Generic;

namespace AtcSimController.SiteReflection
{
    class Environment
    {
        private int[] _activeRunwayIndices;
        private List<Waypoint> _activeRunways;
        private List<Waypoint> _waypoints;
        private Dictionary<string, Waypoint> _waypointDict;

        private int _windHdg;

        /// <summary>
        /// Constructor for the Environment class
        /// </summary>
        /// <param name="waypoints">List of Waypoints on Radar</param>
        public Environment(List<Waypoint> waypoints)
        {
            this._activeRunwayIndices = new int[0];
            this._activeRunways = new List<Waypoint>();
            this._waypoints = waypoints;
            this._waypointDict = new Dictionary<string, Waypoint>();
            this._windHdg = 0;

            // Create a dictionary object with all waypoints for easier access
            foreach(Waypoint wpt in this._waypoints)
            {
                this._waypointDict.Add(wpt.Name, wpt);
            }
        }

        /// <summary>
        /// Returns the waypoint specified, if it exists
        /// </summary>
        /// <param name="name">Name of the Waypoint</param>
        /// <returns>Waypoint object</returns>
        public Waypoint GetWaypoint(string name)
        {
            Waypoint wpt = new Waypoint(null, 1, 0, 0);
            this._waypointDict.TryGetValue(name, out wpt);
            return wpt;
        }

        public override string ToString()
        {
            string ret = "WindHdg: " + this._windHdg;
            ret += "; Active Runways:";
            foreach(Waypoint rw in this._activeRunways)
            {
                ret += " " + rw.Name;
            }

            return ret;
        }

        /// <summary>
        /// Updates information about this environment
        /// </summary>
        /// <param name="newWindHdg">New wind heading</param>
        /// <param name="activeRunwayIndices">Indexes of the new Active Runways in the Waypoints list</param>
        public void Update(int newWindHdg, int[] activeRunwayIndices)
        {
            // Update wind heading
            this._windHdg = newWindHdg;

            // Update active runway Waypoints indices
            this._activeRunwayIndices = activeRunwayIndices;

            // Update active runways list
            this._activeRunways = new List<Waypoint>();

            for(int i=0; i<this._activeRunwayIndices.Length; i++)
            {
                this._activeRunways.Add(this._waypoints[i]);
            }
        }

        /// <summary>
        /// Gets the full list of waypoints for this airport
        /// </summary>
        public List<Waypoint> Waypoints
        {
            get
            {
                return this._waypoints;
            }
        }
    }
}
