namespace AtcSimController.SiteReflection
{
    class Statistics
    {
        private int _landings;
        private int _handoffs;
        private int _improperExits;
        private int _missedApps;
        private int _violationSecs;

        /// <summary>
        /// Base Constructor for the Statistics class
        /// </summary>
        public Statistics()
        {
            this._landings = 0;
            this._handoffs = 0;
            this._improperExits = 0;
            this._missedApps = 0;
            this._violationSecs = 0;
        }
        
        public override string ToString()
        {
            // Compile into string
            string statString =
                "Landings: " + this.Landings
                + "; Handoffs: " + this.Handoffs
                + "; Improper Exits: " + this.ImproperExits
                + "; Missed Approaches: " + this.MissedApproaches
                + "; Violation (secs): " + this.ViolationSeconds;

            return statString;
        }

        /// <summary>
        /// Updates statistics with new values from the game
        /// </summary>
        /// <param name="stats"></param>
        public void UpdateStatistics(int[] stats)
        {
            this._landings = stats[0];
            this._handoffs = stats[1];
            this._improperExits = stats[2];
            this._missedApps = stats[3];
            this._violationSecs = stats[4];
        }
        
        /// <summary>
        /// Gets the number of handoffs performed in this statistic
        /// </summary>
        public int Handoffs {
            get
            {
                return this._handoffs;
            }
        }

        /// <summary>
        /// Gets the number of landings in this statistic
        /// </summary>
        public int Landings
        {
            get
            {
                return this._landings;
            }
        }

        /// <summary>
        /// Gets the number of improper exits that occurred in this statistic
        /// </summary>
        public int ImproperExits
        {
            get
            {
                return this._improperExits;
            }
        }

        /// <summary>
        /// Gets the number of missed approaches that occurred in this statistic
        /// </summary>
        public int MissedApproaches {
            get
            {
                return this._missedApps;
            }
        }

        /// <summary>
        /// Gets the amount of seconds violations were occurring in this statistic
        /// </summary>
        public int ViolationSeconds
        {
            get
            {
                return this._violationSecs;
            }
        }
    }
}
