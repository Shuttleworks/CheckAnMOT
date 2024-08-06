using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CheckAnMOT.Core.Models
{
    public class ResultDTO
    {

        public string Registration { get; set; } = string.Empty;

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Colour { get; set; } = string.Empty;

        public DateTime MotExpiryDate { get; set; }

        public int NumFailedMots { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
