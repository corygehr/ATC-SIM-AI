using AtcSimController.Controller;
using AtcSimController.Controller.Departures;
using AtcSimController.Resources;
using OpenQA.Selenium;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AtcSimController
{
    /// <summary>
    /// Simulation controller class
    /// </summary>
    sealed class Supervisor : IDisposable
    {
        /// <summary>
        /// Cancellation Token which can be used to end the process
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;
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
        /// <param name="driver">Web Driver</param>
        public Supervisor(IWebDriver driver)
        {
            // Store local variables
            this._cancellationTokenSource = new CancellationTokenSource();
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
                while (!this._cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        this._scope.Refresh();
                        this._controller.DoRouting();
                        // Pause before doing next refresh
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(String.Format(Messages.ERROR_BASE, Messages.ERROR_GENERIC));
                        Console.Error.WriteLine(String.Format(Messages.ADDITIONAL_INFO, ex.Message));
                        this._cancellationTokenSource.Cancel();
                    }
                }
            });
        }

        public void Dispose()
        {
            // Dispose objects
            this._cancellationTokenSource.Dispose();
        }
    }
}
