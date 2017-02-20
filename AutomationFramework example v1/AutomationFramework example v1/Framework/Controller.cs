using AutomationFramework_example_v1.Framework.Log.ExampleLogger;
using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutomationFramework_example_v1.Framework
{
    class Controller
    {
        public static IWebDriver driver;
        static Stopwatch suiteTimer;
        static Stopwatch testTimer;
        static Stopwatch stepTimer;
        static Stopwatch identificationTimer;

        public Controller(int controllerOption)
        {
           
                switch (controllerOption)
                {
                    case 0:
                        StandardController();
                        break;
                    default:
                        throw new Exception("Invalid controller option.");
                }
            
        }

        private void StandardController()
        {
            suiteTimer = NewTimer();
            suiteTimer.Start();
            List<Suite> suiteInfo = new Suite().Populate();

            foreach (Suite test in suiteInfo)
            {
                if (test.execute)
                {
                    testTimer = NewTimer();
                    testTimer.Start();
                    test.PopulateLogData();
                    ProjectInfo projectInfo;
                    List<StepInfo> steps;
                    TestInfo testInfo;
                    PopulateTestInfo(test, out testInfo, out projectInfo, out steps);
                    ConsoleLogger.LogTestInfo();
                    LaunchProject(test, projectInfo);

                    foreach (StepInfo step in steps)
                    {
                        ExecuteStep(testInfo, projectInfo, test, step);
                    }
                    testTimer.Stop();
                    ConsoleLogger.LogTestResults();
                    ConsoleLogger.Log();
                }
            }
            suiteTimer.Stop();
            
        }

        private static void ExecuteStep(TestInfo testInfo, ProjectInfo projectInfo, Suite test, StepInfo step)
        {
            stepTimer = NewTimer();
            identificationTimer = NewTimer();
            try
            {
                step.PopulateLogData();
                PreformStep(testInfo, projectInfo, test, step);
                TestLogData.stepResult = "Pass";
            }
            catch (Exception ex)
            {
                TestLogData.testResult = "Fail";
                TestLogData.stepResult = "Fail";
                TestLogData.exceptionMessage = ex.Message;
                if (step.failCondition.ToLower().Equals(""))
                {
                    throw ex;
                }
            }
            finally
            {
                TestLogData.suiteExecutionTime = suiteTimer.Elapsed.ToString();
                TestLogData.testExecutionTime = testTimer.Elapsed.ToString();
                TestLogData.stepExecutionTime = stepTimer.Elapsed.ToString();
                TestLogData.controlIdentificationTime = identificationTimer.Elapsed.ToString();
                ConsoleLogger.LogStepResult();
            }
        }

        private static Stopwatch NewTimer()
        {
            Stopwatch result = new Stopwatch();
            result.Reset();
            return result;
        }

        private static void LaunchProject(Suite test, ProjectInfo projectInfo)
        {
            driver = Driver.GetDriver(test.browser);
            driver.setUrl(projectInfo);
        }

        private static void PopulateTestInfo(Suite test, out TestInfo testInfo, out ProjectInfo projectInfo, out List<StepInfo> steps)
        {
            projectInfo = new ProjectInfo().Populate(test.projectName);
            testInfo = new TestInfo().Populate(test.testName, test.projectName);
            steps = new StepInfo().Populate(testInfo.stepProcedureName);
        }

        public static void PreformStep(TestInfo testInfo, ProjectInfo projectInfo, Suite test, StepInfo step)
        {
            stepTimer.Start();
            ControlInfo controlInfo = null;
            XpathInfo pathInfo = null;
            ActionInfo actionInfo = null;
            KeywordInfo keywordInfo = null;

            PopulateStepInformation(test, step, ref controlInfo, ref pathInfo, ref actionInfo, ref keywordInfo);
            ConsoleLogger.LogStepInfo();
            ExecuteKeyword(testInfo, projectInfo, test, step, keywordInfo);

            ExecuteAction(test, step, controlInfo, pathInfo, actionInfo);
            stepTimer.Stop();
        }

        private static void PopulateStepInformation(Suite test, StepInfo step, ref ControlInfo controlInfo, ref XpathInfo pathInfo, ref ActionInfo actionInfo, ref KeywordInfo keywordInfo)
        {
            if (step.action != "")
            {
                actionInfo = new ActionInfo().Populate(step.action);
            }
            if (step.keyword != "")
            {
                keywordInfo = new KeywordInfo().Populate(step.keyword, test.projectName);
            }

            if (step.controlName != "")
            {
                controlInfo = new ControlInfo().Populate(step.controlName, test.projectName);
                if (controlInfo.xpathName != "")
                {
                    pathInfo = new XpathInfo().Populate(controlInfo.xpathName.ToString(), test.projectName);
                }
            }
        }

        private static void ExecuteKeyword(TestInfo testInfo, ProjectInfo projectInfo, Suite suite, StepInfo step, KeywordInfo keywordInfo)
        {
            if (keywordInfo != null)
            {
                driver.Execute(testInfo, projectInfo, suite, step);
            }
        }

        private static void ExecuteAction(Suite test, StepInfo step, ControlInfo controlInfo, XpathInfo pathInfo, ActionInfo actionInfo)
        {
            if (actionInfo != null)
            {
                if (controlInfo != null)
                {
                    identificationTimer.Start();
                    if (step.parameters.Contains("ifPresent"))
                    {
                        step.parameters.Replace("ifPresent", "").Trim(' ').Trim(',').Trim(' ');
                        if (driver.IsElementPresent(Elements.GetIdentifier(controlInfo, pathInfo)))
                        {
                            IWebElement control = Elements.GetElement(controlInfo, pathInfo);

                            PopulateIdentifiedControlInfo(control);
                            identificationTimer.Stop();
                            control.Execute(step);
                        }
                        else
                        {
                            TestLogData.warning = "The control was not present.";
                        }
                    }
                    else
                    {
                        IWebElement control = Elements.GetElement(controlInfo, pathInfo);

                        PopulateIdentifiedControlInfo(control);
                        identificationTimer.Stop();
                        control.Execute(step);
                    }
                    
                }
                else
                {
                    throw new Exception("Test: " + test.testName + "Error at step number: " + step.id + " - A control name must be provided for an action to be applied to.");
                }
            }
        }

        private static void PopulateIdentifiedControlInfo(IWebElement control)
        {
            TestLogData.identifiedControlTagName = control.TagName;
            TestLogData.identifiedControlId = control.GetAttribute("id");
            TestLogData.identifiedControlName = control.GetAttribute("class");
            TestLogData.identifiedControlText = control.Text;
            TestLogData.identifiedControlIsDisplayed = control.Displayed;
            TestLogData.identifiedControlIsSelected = control.Selected;
            TestLogData.identifiedControlIsEnabled = control.Enabled;
        }
    }
}
