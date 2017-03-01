using AutomationFramework_example_v1.Framework;
using AutomationFramework_example_v1.Framework.Log.ExampleLogger;
using AutomationFramework_example_v1.Framework.SQL.DONT_TOUCH;
using AutomationFramework_example_v1.Framework.SQL.DONT_TOUCH.Bones_Builder;
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
            new Instance();
            Config.CheckAppSettings();
            SQLDataBase.Build();
            new Actions().LoadActionsIntoDB();
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
                try {
                    Driver.driver.Quit();
                    Driver.service.Dispose();
                    KillProcessByID(Driver.serviceId, true);
                }
                catch { }
            }
            //the command line command structure to call the framework would need to be well documented
        }

        static bool KillProcessByID(int id, bool waitForExit = false)
        {
            using (Process p = Process.GetProcessById(id)) {
                if (p == null || p.HasExited) return false;
                p.CloseMainWindow();
                p.Close();
                p.Kill();
                if (waitForExit)
                {
                    p.WaitForExit();
                }
                p.Dispose();
                return true;
            }
        }
    }
}
