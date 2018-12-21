using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Threading.Tasks;

namespace Fileholder.Logging
{
    public class LogManager 
    {
        public static ILog ViewFileLog([CallerLineNumber]int methodLine = 0, [CallerFilePath] string filePath = "")
        {
            return log4net.LogManager.GetLogger("MethodPath: " + filePath + "]\n" + "[MethodLine: " + methodLine.ToString());
        }
    }

}