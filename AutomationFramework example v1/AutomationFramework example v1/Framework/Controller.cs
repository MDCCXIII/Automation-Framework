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

        public void StandardController()
        {
            Stopwatch suiteTimer = NewTimer();
            List<Suite> suiteInfo = new Suite().Populate();

            foreach (Suite test in suiteInfo)
            {
                Stopwatch testTimer = NewTimer();
                if (test.execute)
                {
                    test.PopulateLogData();
                    ProjectInfo projectInfo;
                    List<StepInfo> steps;
                    PopulateTestInfo(test, out projectInfo, out steps);

                    LaunchProject(test, projectInfo);

                    foreach (StepInfo step in steps)
                    {
                        step.PopulateLogData();
                        PreformStep(test, step);
                        ConsoleLogger.Log(new TestLogData());
                    }
                }
                testTimer.Stop();
            }
            suiteTimer.Stop();
        }

        private static Stopwatch NewTimer()
        {
            Stopwatch result = new Stopwatch();
            result.Start();
            return result;
        }

        private static void LaunchProject(Suite test, ProjectInfo projectInfo)
        {
            driver = Driver.GetDriver(test.browser);
            driver.setUrl(projectInfo);
        }

        private static void PopulateTestInfo(Suite test, out ProjectInfo projectInfo, out List<StepInfo> steps)
        {
            projectInfo = new ProjectInfo().Populate(test.projectName);
            TestInfo testInfo = new TestInfo().Populate(test.testName, test.projectName);
            steps = new StepInfo().Populate(testInfo.stepProcedureName);
        }

        private static void PreformStep(Suite test, StepInfo step)
        {
            Stopwatch stepTimer = NewTimer();
            ControlInfo controlInfo = null;
            XpathInfo pathInfo = null;
            ActionInfo actionInfo = null;
            KeywordInfo keywordInfo = null;

            PopulateStepInformation(test, step, ref controlInfo, ref pathInfo, ref actionInfo, ref keywordInfo);

            Stopwatch actionTimer = NewTimer();
            ExecuteKeyword(step, keywordInfo);

            ExecuteAction(test, step, controlInfo, pathInfo, actionInfo);
            actionTimer.Stop();
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

        private static void ExecuteKeyword(StepInfo step, KeywordInfo keywordInfo)
        {
            if (keywordInfo != null)
            {
                driver.Execute(step);
            }
        }

        private static void ExecuteAction(Suite test, StepInfo step, ControlInfo controlInfo, XpathInfo pathInfo, ActionInfo actionInfo)
        {
            if (actionInfo != null)
            {
                if (controlInfo != null)
                {
                    Stopwatch identificationTimer = NewTimer();
                    IWebElement control = Elements.GetElement(controlInfo, pathInfo);
                    identificationTimer.Stop();
                    control.Execute(step);
                }
                else
                {
                    throw new Exception("Test: " + test.testName + "Error at step number: " + step.id + " - A control name must be provided for an action to be applied to.");
                }
            }
        }
    }
}
