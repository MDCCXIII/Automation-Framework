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
            List<Suite> suiteInfo = new Suite().Populate();
           
            foreach (Suite test in suiteInfo)
            { 
                if (test.execute)
                {
                    driver = Driver.GetDriver(test.browser);
                    ProjectInfo projectInfo = new ProjectInfo().Populate(test.projectName);
                    driver.setUrl(projectInfo);
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
                }
            }
        }
    }
}
