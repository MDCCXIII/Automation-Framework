
using AutomationFramework_example_v1.Framework.SQL;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace AutomationFramework_example_v1.Framework
{
    static class Extensions
    {
        public static SqlCommand CheckConnectivity(this SqlCommand cmd)
        {
            if (cmd.Connection == null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[Program.DefaultConnectionStringName].ConnectionString;
                cmd.Connection = new SqlConnection(connectionString);
            }
            return cmd;
        }

        public static SqlCommand CheckConnectivity(this SqlCommand cmd, string connectionName)
        {
            if (cmd.Connection == null)
            {
                if(ConfigurationManager.AppSettings[connectionName] == null)
                {
                    throw new Exception(connectionName + " is not a valid key in the App.Config");
                }
                string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                cmd.Connection = new SqlConnection(connectionString);
            }
            return cmd;
        }

        public static bool columnMatch<T>(this T c, string ColumnName, string fieldname) where T : class 
        {
            return c.GetNameAttribute(fieldname).Equals(ColumnName);
        }

        private static string GetNameAttribute<T>(this T c, string fieldName) where T : class 
        {
            var fieldInfo = typeof(T).GetField(fieldName);
            return ((ColumnMap)Attribute.GetCustomAttribute(fieldInfo, typeof(ColumnMap))).Name;
        }

        public static string DeclaredName(this string field)
        {
            return GetFieldName(() => field);
        }

        private static string GetFieldName<T>(Expression<Func<T>> expr)
        {
            var body = ((MemberExpression)expr.Body);
            return body.Member.Name;
        }

        public static void Wait(this IWebDriver driver, int timeoutInSeconds)
        {
            for (var i = 0; i < timeoutInSeconds; i++)
            {
                Thread.Sleep(1000);
            }
        }

        public static bool IsElementPresent(this IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IWebElement FindElement(this IWebDriver driver, string Xpath, string[] parameters)
        {
            return driver.FindElement(By.XPath(string.Format(Xpath, parameters)));
        }

        public static IWebElement FindElement(this IWebDriver driver, string Xpath, string[] parameters, int timeoutInSeconds)
        {
            return driver.FindElement(By.XPath(string.Format(Xpath, parameters)), timeoutInSeconds);
        }



        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                try
                {
                    for (var i = 0; i < timeoutInSeconds; i++)
                    {
                        if (driver.IsElementPresent(by)) return driver.FindElement(by);
                        Thread.Sleep(1000);
                    }
                }
                catch (NoSuchElementException)
                {

                }
            }
            return driver.FindElement(by);
        }

        public static bool TextContains(this IWebElement element, string text)
        {
            bool result = false;
            if (element.Text.Contains(text))
            {
                result = true;
            }

            return result;
        }

        public static bool TextEqual(this IWebElement element, string text)
        {
            bool result = false;
            if (element.Text.Equals(text))
            {
                result = true;
            }

            return result;
        }

        public static void SelectOptionByText(this IWebElement element, string optionText)
        {
            new SelectElement(element).SelectByText(optionText);
        }
    }
}
