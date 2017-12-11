using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Utility.Common
{
    public static class ExceptionHandler
    {
        public static string GetExceptionMessageDeeper(Exception E)
        {
            string message = E.Message;

            if (E.InnerException != null)
            {
                message = message + ". " + GetExceptionMessageDeeper(E.InnerException);
            }

            return message;
        }
    }
}
