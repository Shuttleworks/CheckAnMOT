using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckAnMOT.Core.Models
{
    public class ApiError
    {
        /* To match 404 Json from API */
         
        public string HttpStatus { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string AwsRequestId { get; set; } = string.Empty;
    }
}
