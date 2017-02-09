using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1
{
    class StepInfo
    {
        //captured by selenium using control info and pathinfo
        public IWebElement element;

        #region [ProjectName]_Control_Information table

        public string controlId;

        public string identifyingNodeType;

        public string identifyingParameterName;

        public string identifyingParameterValue;

            #region [projectName]_path_Information

        public string pathId;
                public string path;

                public string pathType;

            #endregion
        #endregion

        //taken from selenium
        public string actualValue;

        #region [TestName]_steps
        public string controlName;

        public string expectedValue;

        public string stepNumber;

        public string action;

        public string keyword;

        public string parameters;

        #endregion
    }
}
