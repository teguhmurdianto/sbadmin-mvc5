using Company.Project.Access;
using Company.Project.Object.General;
using Company.Project.Utility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Business
{
    public class BooksBusinessLogic
    {
        public BooksBusinessLogic() 
        {
        }

        public ResultStatus UpdateBooks(string guid) 
        {
            ResultStatus resultStat = new ResultStatus(false);
            BooksDataAccess booksDAL = new BooksDataAccess();

            try
            {
                int i = booksDAL.GetBooks(guid);
                if (i == 0) 
                {
                    resultStat.IsSuccess = true;
                }
            }
            catch (Exception E) 
            {
                resultStat.IsSuccess = false;
                resultStat.ErrorMessage = ExceptionHandler.GetExceptionMessageDeeper(E);
            }

            return resultStat;
        }
    }
}
