using AtcSimController.AIController;
using AtcSimController.Resources;
using AtcSimController.SiteReflection.SimConnector;
using AtcSimController.SiteReflection.SimConnector.Resources;

using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System;

namespace AtcSimController
{
    /// <summary>
    /// Entry point for the application
    /// </summary>
    class AIEntry
    {
        static void Main(string[] args)
        {
            // Application Start
            Console.WriteLine(Messages.CREDIT + "\n");

            if (args.Length == 5)
            {
                #region Start Selenium (Web Reflection service)

                // Setup for the ATC-SIM Environment
                Console.WriteLine(Messages.SELENIUM_SETUP + "\n");

                IWebDriver driver;

                switch (args[0])
                {
                    case "chrome":
                        // Prepare Chrome settings
                        driver = new ChromeDriver();
                        driver.Url = "http://atc-sim.com/";
                        driver.Navigate();
                        break;

                    default:
                    case "ie":
                        // Prepare IE settings
                        InternetExplorerOptions ieOptions = new InternetExplorerOptions
                        {
                            IgnoreZoomLevel = true,
                            InitialBrowserUrl = "http://atc-sim.com/",
                            PageLoadStrategy = PageLoadStrategy.Eager // Wait for pages to be 'complete'
                        };

                        // Create a new instance of the IE WebDriver
                        driver = new InternetExplorerDriver(ieOptions);
                        break;
                }

                // Wait for the page to load
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until((d) => {
                    try
                    {
                        return (d.FindElement(By.Id(FormElements.OPTIONS_FORM_ID)) != null);
                    }
                    catch (NoSuchElementException)
                    {
                        // Do nothing, keep waiting
                        return false;
                    }
                });

                #endregion
                #region Configure Simulator

                try
                {
                    /// AIRPORT
                    // Change the value of the airport selector
                    UIHelper.ChangeDropDownByValue(ref driver, FormElements.AIRPORT_SELECTION_ID, args[1]);

                    /// IATA Preference
                    if (Boolean.Parse(args[2]))
                    {
                        UIHelper.ChangeRadioByValue(ref driver, FormElements.AIRLINE_FORMAT_ID, "IATA");
                    }

                    /// WIND DIRECTION
                    // Get element for wind direction
                    UIHelper.ChangeDropDownByValue(ref driver, FormElements.WIND_CHANCE_ID, args[3]);

                    // REALISM
                    string playMoveValue;
                    switch (args[4])
                    {
                        case "easy":
                            playMoveValue = "1";
                            break;

                        case "arrivals":
                            playMoveValue = "2";
                            break;

                        case "departures":
                            playMoveValue = "3";
                            break;

                        default:
                            playMoveValue = "0";
                            break;
                    }

                    UIHelper.ChangeDropDownByValue(ref driver, FormElements.DIFFICULTY_ID, playMoveValue);

                    /// SCALE MARKERS
                    /// We change this automatically, no option provided to end-user
                    UIHelper.ChangeDropDownByValue(ref driver, FormElements.SCALE_MARKERS_ID, "2");

                    // Submit form
                    Console.WriteLine(String.Format("\n{0}...", Messages.SETTINGS_SUBMITTED));
                    UIHelper.SubmitForm(ref driver, FormElements.OPTIONS_FORM_ID);
                    Console.WriteLine(Messages.ENVIRONMENT_LOAD);

                    // Wait for the new page to load
                    wait.Until((d) =>
                    {
                        try
                        {
                            return (d.FindElement(By.Name(FormElements.CLEARANCE_TEXTBOX_ID)) != null);
                        }
                        catch (NoSuchElementException)
                        {
                            // Do nothing, keep waiting
                            return false;
                        }
                    });
                }
                catch (NoSuchElementException ex)
                {
                    Console.Error.WriteLine(String.Format(Messages.ERROR_BASE, Messages.ENVIRONMENT_LOAD_FAIL));
                    Console.Error.WriteLine(String.Format(Messages.ADDITIONAL_INFO, ex.Message));
                    return;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(String.Format(Messages.ERROR_BASE, Messages.ERROR_GENERIC));
                    Console.Error.WriteLine(String.Format(Messages.ADDITIONAL_INFO, ex.Message));
                    return;
                }

                #endregion

                #region Traffic Controller Active

                // Start controller
                Controller controller = new Controller(driver);
                controller.Run();

                // One Run() has finished, exit
                driver.Quit();

                Console.WriteLine(String.Format("\n--{0}--", Messages.SELENIUM_DISCONNECTED));

                #endregion
            }
            else
            {
                #region Usage Options output
                // Output usage options
                Console.WriteLine("Usage:\n\tBROWSER AIRPORT AIRLINEIATAOPT WIND REALISM\n\t(All arguments required)");
                Console.WriteLine("Arguments:");
                Console.WriteLine("\tBROWSER - Browser to use (chrome, ie)");
                Console.WriteLine("\tAIRPORT - ICAO Code for Target Airport (See website for list)");
                Console.WriteLine("\tAIRLINEIATAOPT - Use IATA Codes for Airlines (True or False)");
                Console.WriteLine("\tWIND - Wind Change Frequency (0, 10, 25, 50, 75, 100)");
                Console.WriteLine("\tREALISM - Realism Settings (normal, easy, arrivals, departures)");
                return;
                #endregion
            }
        }
    }
}
