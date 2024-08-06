using CheckAnMOT.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckAnMOT.Core.Services
{
    public interface IMotService
    {

        public Task<ResultDTO> GetVehicleMot(string numberPlate);

        public string GetAPIKey();

        public string GetAPIUrl();
    }
}
