using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming.Payroll
{
    // Class for Pay Calculator without a tax threshold
    public class PayCalculatorNoThreshold : PayCalculator
    {
        // Constructor to initialize properties using base constructor
        public PayCalculatorNoThreshold(float grossPay, float taxRatesA, float taxRatesB, double totalPay)
            : base(grossPay, taxRatesA, taxRatesB, totalPay)
        {
        }

        // Override method to calculate pay without a tax threshold
        public override float CalculatePay()
        {
            // Add the net wage calculation code for cases with no tax threshold.
            return grossPay - CalculateTax() - CalculateSuperannuation();
        }

        // Override method to calculate tax without a tax threshold
        public override float CalculateTax()
        {
            // Add tax calculation code for cases with no tax threshold.
            float x = grossPay + 0.99f; // x is the number of whole dollars of the weekly earnings plus 99 cents
            return taxRatesA * x - taxRatesB;
        }

        // Override method to calculate superannuation without a tax threshold
        public override float CalculateSuperannuation()
        {
            // Add superannuation calculation code for cases with no tax threshold.
            double v = totalPay * 0.115;
            float x = (float)v;
            return x;
        }
    }
}
