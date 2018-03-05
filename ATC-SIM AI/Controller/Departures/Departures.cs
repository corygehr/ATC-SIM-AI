using System;
using System.Collections.Generic;

namespace AtcSimController.Controller.Departures
{
    public class Departures : TrafficController
    {
        private Dictionary<string, Queue<string>> _takeoffQueue = new Dictionary<string, Queue<string>>();

        /// <summary>
        /// Creates a new <see cref="Departures"/> controller
        /// </summary>
        /// <param name="scope">Radar Scope object</param>
        public Departures(RadarScope scope) : base(scope)
        {
            // Create queue for each runway
        }

        public override void DoRouting()
        {
            throw new NotImplementedException();
        }
    }
}