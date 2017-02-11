using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;


namespace AutomationFramework_example_v1.Framework
{
    class Controller
    {
        public static IWebDriver driver;

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
                    driver = Driver.GetDriver(test.browser);
                    ProjectInfo projectInfo = new ProjectInfo().Populate(test.projectName);
                    driver.Url = projectInfo.Url;
                    TestInfo testInfo = new TestInfo().Populate(test.testName, test.projectName);
                    List<StepInfo> steps = new StepInfo().Populate(testInfo.stepProcedureName);
                    foreach (StepInfo step in steps)
                    {
                        ControlInfo controlInfo = null;
                        PathInfo pathInfo = null;
                        ActionInfo actionInfo = null;
                        KeywordInfo keywordInfo = null;

                        if (step.controlName != "")
                        {
                            controlInfo = new ControlInfo().Populate(step.controlName, test.projectName);
                            if (controlInfo.pathName != "")
                            {
                                pathInfo = new PathInfo().Populate(controlInfo.pathName.ToString(), test.projectName);
                            }
                        }
                        if(step.action != "")
                        {
                            actionInfo = new ActionInfo().Populate(step.action);
                        }
                        if(step.keyword != "")
                        {
                            keywordInfo = new KeywordInfo().Populate(step.keyword, test.projectName);
                        }

                        if(keywordInfo != null)
                        {
                            driver.Execute(step);
                        }
                        if(actionInfo != null)
                        {
                            if(controlInfo != null)
                            {
                                IWebElement control = Elements.GetElement(controlInfo, pathInfo);
                                control.Execute(step);
                            }
                            else
                            {
                                throw new Exception("Test: " + test.testName + "Error at step number: " + step.stepNumber + " - A control name must be provided for an action to be applied to.");
                            }
                        }
                    }
                    //foreach test:
                    //start a timer to record test execution time
                    //read associated project and environment information
                    //read test data 
                    //launch the project in environment
                    
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
