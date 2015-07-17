using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ATC_SIM_AI
{
    class UIHelper
    {
        /// <summary>
        /// Changes a DropDown selector by value
        /// </summary>
        /// <param name="driver">Current Web Driver</param>
        /// <param name="ddName">'name' value for the selector</param>
        /// <param name="ddValue">Target value</param>
        public static void ChangeDropDownByValue(ref IWebDriver driver, String ddName, String ddValue)
        {
            // Target element
            IWebElement targetElement = driver.FindElement(By.Name(ddName));
            // Create a select element
            SelectElement targetSelector = new SelectElement(targetElement);
            // Change to the value
            targetSelector.SelectByValue(ddValue);
        }

        /// <summary>
        /// Changes a Radio value
        /// </summary>
        /// <param name="driver">Current Web Driver</param>
        /// <param name="rdoName">'name' value for the radio selector</param>
        /// <param name="rdoValue">Target value</param>
        public static void ChangeRadioByValue(ref IWebDriver driver, String rdoName, String rdoValue)
        {
            // Get element for the option
            IList<IWebElement> radioOptions = driver.FindElements(By.Name(rdoName));

            foreach (IWebElement option in radioOptions)
            {
                // Click the option we want to select
                if (option.GetAttribute("value") == rdoValue)
                {
                    option.Click();
                    // Stop looping - we found our match
                    return;
                }
            }
        }

        /// <summary>
        /// Enters text and submits the form
        /// </summary>
        /// <param name="driver">Current Web Driver</param>
        /// <param name="element">Name of TextBox element</param>
        /// <param name="text">Text to Enter</param>
        public static void EnterTextSubmitForm(IWebDriver driver, String element, String text)
        {
            // Get the textbox element
            IWebElement textBox = driver.FindElement(By.Name(element));
            // To speed up input times, use JS as a workaround
            (driver as IJavaScriptExecutor).ExecuteScript(
                "document.getElementsByName('txtClearance')[0].setAttribute('value', '" + text + "')");
            textBox.Submit();
        }

        /// <summary>
        /// Submits a web form
        /// </summary>
        /// <param name="driver">Current Web Driver</param>
        /// <param name="frmId">ID of the form to submit</param>
        public static void SubmitForm(ref IWebDriver driver, String frmId)
        {
            // Look for the form element and submit
            driver.FindElement(By.Id(frmId)).Submit();
        }
    }
}
