using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGM_MS_Support.BusinessLogic.Entities
{
    public  class PendingCases
    {
        public string ApplicationNumber { get; set; }   
        public string JourneyStatus { get; set; }
        public string SubmittedDate { get; set; }
        public string CurrentDate { get; set; }
        public string TimeDiffInMinutes {  get; set; }
    }
}
