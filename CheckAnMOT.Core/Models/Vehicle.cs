using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckAnMOT.Core.Models
{
    public class Vehicle
    {
        /* To match properties in 200 response Json from API */
        public string Registration { get; set; } = String.Empty;

        public string Make { get; set; } = String.Empty;

        public string Model { get; set; } = String.Empty;

        public string FirstUsedDate { get; set; } = String.Empty;

        public string FuelType { get; set; } = String.Empty;

        public string PrimaryColour { get; set; } = String.Empty;

        public string MotTestExpiryDate { get; set; } = String.Empty;

        public List<MotTest> MotTests { get; set; }

        public Vehicle()
        {
            MotTests = new List<MotTest>();
        }
    }
}
