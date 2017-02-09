using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutomationFramework_example_v1
{
    class Controller
    {
        public Controller()
        {
            //TODO: start a timer to record test suite execution time
            //Read data storage information for automation test suites
            List<Suite> suiteInfo = new Suite().Populate();
           
            //foreach test suite listed to execute
            foreach (Suite test in suiteInfo)
            { 
                
                //TODO: read test suite data for individual tests to execute
                if (test.execute)
                {
                    //foreach test:
                    //start a timer to record test execution time
                    //read associated project and environment information
                    //read test data 
                    //launch the project in environment
                    IWebDriver driver = Driver.GetDriver(test.browser);
                    driver.setUrl(test.projectName);
                }
               
            }
            
            
            
            //{
           
            
            //foreach test step
            //{
            //start a timer to record total step execution time
            //read test step data
            //read associated action data
            //read associated keyword data
            //read associated user information
            //start a timer to record command execution time
            //execute step command
            //stop command execution timer
            //stop total step execution timer
            //Log information using current timer ticks as current information
            //}
            //stop test execution timer
            //Log test execution time
            //}
            //stop test suite execution timer
            //log test suite execution time
            //}

            //cleanup and exit framework
        }
        
    }
}
