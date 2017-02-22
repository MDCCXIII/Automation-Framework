﻿using AutomationFramework_example_v1.Framework;
using AutomationFramework_example_v1.Framework.Log.ExampleLogger;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutomationFramework_example_v1
{
    class Program
    {
        /// <summary>
        /// The default connection string name from the App config.
        /// </summary>
       // public const string DefaultConnectionStringName = "Tony'sLocalConnection";
        public const string DefaultConnectionStringName = "SharedServer";
        public const int DefaultController = 0;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        static void Main(string[] args)
        {
            //use args to set up framework properties like debugging and show console ect.
            try
            {
                //call the appropriate controller method, this may also be controled by argument
                new Controller(DefaultController);
            }
            catch (Exception ex)
            {
                ConsoleLogger.LogTestResults();
                ConsoleLogger.Log(ex);
                ConsoleLogger.Log();
                
            }
            finally
            {
                try { Process.GetProcessById(Driver.serviceId).Kill(); }
                catch { }
            }
            //the command line command structure to call the framework would need to be well documented
        }
    }
}
