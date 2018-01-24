using AtcSimController.AIController;
using AtcSimController.SiteReflection.SimConnector;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace AtcSimController
{
    class AIEntry
    {
        static void Main(string[] args)
        {
            // Application Start
            Console.WriteLine("ATC-SIM AI By Cory Gehr (2015)\n");

            if (args.Length == 5)
            {
                // Setup for the ATC-SIM Environment
                Console.WriteLine("Starting Selenium Server...\n");

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
                        return (d.FindElement(By.Id("frmOptions")) != null);
                    }
                    catch (NoSuchElementException)
                    {
                        // Do nothing, keep waiting
                        return false;
                    }
                });

                //
                // CONFIGURE SIMULATOR
                //

                try
                {
                    /// AIRPORT
                    // Change the value of the airport selector
                    UIHelper.ChangeDropDownByValue(ref driver, "selAirport", args[1]);

                    /// IATA Preference
                    if (Boolean.Parse(args[2]))
                    {
                        UIHelper.ChangeRadioByValue(ref driver, "rdoICAOorIATA", "IATA");
                    }

                    /// WIND DIRECTION
                    // Get element for wind direction
                    UIHelper.ChangeDropDownByValue(ref driver, "WindChance", args[3]);

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

                    UIHelper.ChangeDropDownByValue(ref driver, "PlayMode", playMoveValue);

                    /// SCALE MARKERS
                    /// We change this automatically, no option provided to end-user
                    UIHelper.ChangeDropDownByValue(ref driver, "ScaleMarks", "2");

                    // Submit form
                    Console.WriteLine("\nSubmitting settings form...");
                    UIHelper.SubmitForm(ref driver, "frmOptions");
                    Console.WriteLine("Loading radar scope...");

                    // Wait for the new page to load
                    wait.Until((d) =>
                    {
                        try
                        {
                            return (d.FindElement(By.Name("frmClearance")) != null);
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
                    Console.Error.WriteLine("FATAL: One or more required form elements could not be found. Cannot proceed.");
                    Console.Error.WriteLine("Additional information: " + ex.Message + "\n");
                    return;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("FATAL: A general exception has occurred. Cannot continue.");
                    Console.Error.WriteLine("Additional information: " + ex.Message + "\n");
                    return;
                }

                // Start controller
                Controller controller = new Controller(driver);
                controller.Run();

                // One Run() has finished, exit
                driver.Quit();

                Console.WriteLine("\n--Disconnected--");
            }
            else
            {
                // Output usage options
                Console.WriteLine("Usage:\n\tBROWSER AIRPORT AIRLINEIATAOPT WIND REALISM\n\t(All arguments required)");
                Console.WriteLine("Arguments:");
                Console.WriteLine("\tBROWSER - Browser to use (chrome, ie)");
                Console.WriteLine("\tAIRPORT - ICAO Code for Target Airport (See website for list)");
                Console.WriteLine("\tAIRLINEIATAOPT - Use IATA Codes for Airlines (True or False)");
                Console.WriteLine("\tWIND - Wind Change Frequency (0, 10, 25, 50, 75, 100)");
                Console.WriteLine("\tREALISM - Realism Settings (normal, easy, arrivals, departures)");
                return;
            }
        }
    }
}
