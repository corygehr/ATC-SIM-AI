using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace ATC_SIM_AI
{
    class AIEntry
    {
        static void Main(string[] args)
        {
            // Application Start
            Console.WriteLine("ATC-SIM AI By Cory Gehr (2015)\n");

            if (args.Length > 0)
            {
                // Setup for the ATC-SIM Environment
                
                // Prepare IE settings
                InternetExplorerOptions ieOptions = new InternetExplorerOptions
                {
                    IgnoreZoomLevel = true,
                    InitialBrowserUrl = "http://atc-sim.com/"
                };

                // Create a new instance of the IE WebDriver
                IWebDriver driver = new InternetExplorerDriver(ieOptions);

                //
                // CONFIGURE SIMULATOR
                //

                /// AIRPORT
                /// 

                // Change the value of the airport selector
                UIHelper.ChangeDropDownByValue(ref driver, "selAirport", args[0]);

                /// IATA Preference

                if (Boolean.Parse(args[1]))
                {
                    UIHelper.ChangeRadioByValue(ref driver, "rdoICAOorIATA", "IATA");
                }

                /// WIND DIRECTION

                // Get element for wind direction
                UIHelper.ChangeDropDownByValue(ref driver, "WindChance", args[2]);

                // REALISM
                String playMoveValue;
                switch (args[3])
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
                Console.WriteLine("Submitting settings form...");
                UIHelper.SubmitForm(ref driver, "frmOptions");

                // Start controller
                Controller controller = new Controller(driver);
                controller.Run();
            }
            else
            {
                // Output usage options
                Console.WriteLine("Usage:\n\tAIRPORT AIRLINEIATAOPT WIND REALISM\n\t(All arguments required)");
                Console.WriteLine("Arguments:");
                Console.WriteLine("\tAIRPORT - ICAO Code for Target Airport (See website for list)");
                Console.WriteLine("\tAIRLINEIATAOPT - Use IATA Codes for Airlines (True or False)");
                Console.WriteLine("\tWIND - Wind Change Frequency (0, 10, 25, 50, 75, 100)");
                Console.WriteLine("\tREALISM - Realism Settings (normal, easy, arrivals, departures)");
            }
        }
    }
}
