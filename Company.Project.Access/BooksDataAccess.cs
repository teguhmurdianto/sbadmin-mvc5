using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Connection;
using Company.Project.Utility;
using Company.Project.Utility.Common;

namespace Company.Project.Access
{
    public class BooksDataAccess
    {
        public int GetBooks(string guid) 
        {
            int result = -1;

            try
            {
                using (var db = ProjectDBContext.Create())
                {
                    result = db.Database.ExecuteSqlCommand("update MasterUser set ModifiedBy = 'teguh'");
                }
            }
            catch (Exception E)
            {
                throw new Exception(ExceptionHandler.GetExceptionMessageDeeper(E));
            }

            return result;
        }
    }
}
