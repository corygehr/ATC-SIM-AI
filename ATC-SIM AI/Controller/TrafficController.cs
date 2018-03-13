using AtcSimController.Resources;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AtcSimController.Controller
{
    /// <summary>
    /// Traffic Controller base class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class TrafficController
    {
        /// <summary>
        /// Internal simulation data object
        /// </summary>
        /// <remarks>
        /// Allows internal assignment but not external Controller manipulation
        /// </remarks>
        private RadarScope _scope { get; set; }
        /// <summary>
        /// Simulation Environment data
        /// </summary>
        protected RadarScope Scope
        {
            get
            {
                if (_scope != null)
                {
                    return _scope;
                }
                else
                {
                    throw new ApplicationException(Messages.BROKER_CONNECTION_FAIL);
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="TrafficController"/> instance
        /// </summary>
        /// <param name="scope">Scope object</param>
        public TrafficController(RadarScope scope)
        {
            this._scope = scope;
        }

        /// <summary>
        /// Executes flight routing actions
        /// </summary>
        public abstract void DoRouting();
    }
}
