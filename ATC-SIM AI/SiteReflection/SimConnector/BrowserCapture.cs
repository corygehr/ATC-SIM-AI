using OpenQA.Selenium;

namespace AtcSimController.SiteReflection.SimConnector
{
    sealed class BrowserCapture
    {
        private IWebDriver _driver;
        private IJavaScriptExecutor _js;

        /// <summary>
        /// Constructor for the SimCapture class
        /// </summary>
        /// <param name="driver">Current web driver</param>
        public BrowserCapture(IWebDriver driver)
        {
            this._driver = driver;
            this._js = driver as IJavaScriptExecutor;
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
        /// Tells if the simulator is still active
        /// </summary>
        /// <returns>True if active, false if not</returns>
        public bool SimActive()
        {
            return true;
        }
    }
}
