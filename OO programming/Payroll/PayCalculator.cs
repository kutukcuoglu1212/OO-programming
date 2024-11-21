using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming.Payroll
{

    // Base class for Pay Calculator
    public class PayCalculator
    {
        // Fields to store gross pay and tax rates
        protected float grossPay;
        protected double totalPay;
        protected float taxRatesA;
        protected float taxRatesB;

        // Constructor to initialize base pay calculator properties
        public PayCalculator(float grossPay, float taxRatesA, float taxRatesB, double totalPay)
        {
            this.totalPay = totalPay;
            this.grossPay = grossPay;
            this.taxRatesA = taxRatesA;
            this.taxRatesB = taxRatesB;
        }


        // Virtual method to calculate pay
        public virtual float CalculatePay()
        {
            return 0.0f;
        }

        // Virtual method to calculate tax
        public virtual float CalculateTax()
        {
            return 0.0f;
        }

        // Virtual method to calculate superannuation
        public virtual float CalculateSuperannuation()
        {
            return 11.5f;
        }
    }
}
