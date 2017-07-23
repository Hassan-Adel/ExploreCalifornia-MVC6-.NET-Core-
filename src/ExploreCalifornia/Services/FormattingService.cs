using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Services
{
    //This class can be added in anywhere not necessarily in a services folder
    public class FormattingService
    {
        public string AsReadableDate(DateTime date) {
            return date.ToString("d");
        }
    }
}
