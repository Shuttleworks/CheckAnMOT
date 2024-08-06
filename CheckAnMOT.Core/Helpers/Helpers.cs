using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CheckAnMOT.Core.Helpers
{
    public class Helpers
    {
        public static bool ValidRegPlate(string numberplate)
        {
            string pattern = @"\W";

            if(Regex.IsMatch(numberplate, pattern) || numberplate.Length > 7)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string RemoveUserWhiteSpace(string numberplate)
        {
            if (!string.IsNullOrEmpty(numberplate))
            {
                numberplate = numberplate.Replace(" ", "").Trim();
            }
            return numberplate;
        }
        
    }
}
