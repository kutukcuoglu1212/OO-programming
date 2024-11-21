using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming.Payroll
{
    // Class for Pay Calculator with a tax threshold
    public class PayCalculatorWithThreshold : PayCalculator
    {
        // Constructor to initialize properties using base constructor
        public PayCalculatorWithThreshold(float grossPay, float taxRatesA, float taxRatesB, double totalPay)
            : base(grossPay, taxRatesA, taxRatesB, totalPay)
        {
        }

        // Override method to calculate pay with a tax threshold
        public override float CalculatePay()
        {
            // Add the net wage calculation code for cases with a tax threshold.
            return grossPay - CalculateTax() - CalculateSuperannuation();
        }

        // Override method to calculate tax with a tax threshold
        public override float CalculateTax()
        {
            // Add tax calculation code for states with tax thresholds.
            float x = grossPay + 0.99f; // x is the number of whole dollars of the weekly earnings plus 99 cents
            return taxRatesA * x - taxRatesB;
        }

        // Override method to calculate superannuation with a tax threshold
        public override float CalculateSuperannuation()
        {
            // Add superannuation calculation code for states with tax thresholds.
            double v = totalPay * 0.115;
            float x = (float)v;
            return x;
        }
    }
}
