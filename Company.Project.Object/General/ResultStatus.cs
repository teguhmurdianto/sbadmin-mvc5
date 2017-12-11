using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Object.General
{
    public class ResultStatus
    {
        private string _message { get; set; }
        private string _errorMessage { get; set; }

        public ResultStatus(bool isSuccess) 
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }

        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(_message)) { return string.Empty; }
                else { return _message; }
            }
            set
            {
                if (string.IsNullOrEmpty(value)) { _message = string.Empty; }
                else { _message = value; }
            }
        }

        public string ErrorMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_errorMessage)) { return string.Empty; }
                else { return _errorMessage; }
            }
            set
            {
                if (string.IsNullOrEmpty(value)) { _errorMessage = string.Empty; }
                else { _errorMessage = value; }
            }
        }
    }
}
