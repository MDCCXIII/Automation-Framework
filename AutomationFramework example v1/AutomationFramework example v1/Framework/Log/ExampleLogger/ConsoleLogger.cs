using AutomationFramework_example_v1.Framework.Log.LogObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework.Log.ExampleLogger
{
    static class ConsoleLogger
    {

        public static void Log(TestLogData c)
        {
            string result = "\n";
            foreach(FieldInfo f in c.GetType().GetFields())
            {
                if (f.GetValue(c) != null && !f.GetValue(c).ToString().Equals("") && !f.GetValue(c).ToString().Equals("False") && !f.GetValue(c).ToString().Equals("00:00:00"))
                {
                    result += f.Name + ": " + f.GetValue(c).ToString() + ", ";
                }
            }
            Log(result.TrimEnd().TrimEnd(','));
        }

        public static void Log(string logMessage)
        {
            Console.WriteLine(logMessage);
        }
    }
}
