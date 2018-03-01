using System;
using System.Threading.Tasks;

namespace AtcSimController.Controller.Departures
{
    public class Departures : TrafficController
    {
        /// <summary>
        /// Creates a new <see cref="Departures"/> controller
        /// </summary>
        /// <param name="scope">Radar Scope object</param>
        public Departures(RadarScope scope) : base(scope)
        {

        }

        public override Task DoRouting()
        {
            throw new NotImplementedException();
        }
    }
}
