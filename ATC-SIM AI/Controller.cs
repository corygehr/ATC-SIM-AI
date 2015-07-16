using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace ATC_SIM_AI
{
    class Controller
    {
        /// <summary>
        /// WebDriver object
        /// </summary>
        private IWebDriver driver;

        /// <summary>
        /// Constructor for the Controller class
        /// </summary>
        /// <param name="driver">Current Web Driver object</param>
        public Controller(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Begin the simulator
        /// </summary>
        public void Run()
        {
            // Environment setup
        }
    }
}
