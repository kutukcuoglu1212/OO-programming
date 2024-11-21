using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming.Tax
{

    // Class representing tax rates without a threshold
    public class TaxRateNoThreshold
    {
        public float WeeklyEarningsLower { get; set; }
        public float WeeklyEarningsUpper { get; set; }
        public float RateA { get; set; }
        public float RateB { get; set; }
    }
}
