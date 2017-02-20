using AutomationFramework_example_v1.Framework;
using System;
using System.Diagnostics;

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

        static void Main(string[] args)
        {

            //use args to set up framework properties like debugging and show console ect.

            //call the appropriate controller method, this may also be controled by argument
            try
            {
new Controller(DefaultController);
            }
            
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
            //the command line command structure to call the framework would need to be well documented
        }
    }
}
