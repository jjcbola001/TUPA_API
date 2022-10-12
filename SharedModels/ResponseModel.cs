using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class ResponseModel<T>
    {
        public string Token { get; set; }
        public bool ReturnStatus { get; set; }
        public string ReturnMessage { get; set; }
        public List<string> Errors;
        public int TotalPages;
        public int TotalRows;
        public int PageSize;
        public Boolean IsAuthenicated;
        public T Entity;

        public ResponseModel()
        {
            ReturnMessage = "An error occur while processing!";
            ReturnStatus = false;
            Errors = new List<string>();
            TotalPages = 0;
            PageSize = 0;
            IsAuthenicated = false;
        }
    }
}
