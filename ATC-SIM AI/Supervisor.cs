using AtcSimController.Controller;
using AtcSimController.Controller.Departures;
using OpenQA.Selenium;
using System.Threading;
using System.Threading.Tasks;

namespace AtcSimController
{
    /// <summary>
    /// Simulation controller class
    /// </summary>
    sealed class Supervisor
    {
        /// <summary>
        /// Cancellation Token which can be used to end the process
        /// </summary>
        private CancellationToken _cancellationToken;
        /// <summary>
        /// Controller used to direct air traffic
        /// </summary>
        private TrafficController _controller;
        /// <summary>
        /// Web Driver object
        /// </summary>
        private IWebDriver _driver;
        /// <summary>
        /// Radar Scope
        /// </summary>
        private readonly RadarScope _scope;
        /// <summary>
        /// Allow other parts of the application to have readonly access to the RadarScope object
        /// </summary>
        public RadarScope Scope
        {
            get
            {
                return this._scope;
            }
        }

        /// <summary>
        /// Creates a new <see cref="Supervisor"/> class to run the simulation
        /// </summary>
        public Supervisor(IWebDriver driver, CancellationToken cancellationToken)
        {
            this._driver = driver;
            // Create new Scope object
            this._scope = new RadarScope(driver);
            // Create controller object and give it the radar scope to use
            this._controller = new Departures(this._scope);
        }

        /// <summary>
        /// Runs the simulation
        /// </summary>
        public async Task Run()
        {
            // Loop scope refreshing
            await Task.Run(() =>
            {
                while (!this._cancellationToken.IsCancellationRequested)
                {
                    this._scope.Refresh();
                    this._controller.DoRouting();
                    // Pause 1sec before doing next automatic refresh
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
