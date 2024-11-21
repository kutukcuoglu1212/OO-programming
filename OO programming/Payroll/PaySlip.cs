using OO_programming.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming.Payroll
{
    // Class representing a Pay Slip with associated payment details
    public class PaySlip
    {
        // Properties to store basic pay slip information
        public float HourlyRate { get; set; }
        public float HoursWorked { get; set; }
        public string TaxRate { get; set; }
        public float rateA { get; set; }
        public float rateB { get; set; }

        public float GrossPay { get; set; }
        public float NetPay { get; private set; }
        public float TaxAmount { get; private set; }
        public float Superannuation { get; private set; }

        //test
        public float calculatedSuperannuation { get; set; }
        public float TotalPay { get; private set; }
        public float taxPerHour { get; set; }
        public float calculatedGrossPay { get; set; }

        // Constructor to calculate pay slip details based on tax rate
        public PaySlip(float hourlyRate, float hoursWorked, string taxRate, List<TaxRateWithThreshold> taxRatesWithThreshold, List<TaxRateNoThreshold> taxRatesNoThreshold)
        {
            // Initialize basic pay slip properties
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
            TaxRate = taxRate;
            TotalPay = hourlyRate * hoursWorked;
            GrossPay = TotalPay - TaxAmount + calculatedSuperannuation;


            // Choose appropriate calculator based on tax rate presence
            if (TaxRate == "Y")
            {
                foreach (var rate in taxRatesWithThreshold)
                {
                    if (GrossPay >= rate.WeeklyEarningsLower && GrossPay <= rate.WeeklyEarningsUpper)
                    {
                        rateA = rate.RateA;
                        rateB = rate.RateB;
                    }
                }

                var calculator = new PayCalculatorWithThreshold(GrossPay, rateA, rateB, TotalPay);
                CalculatePayrollWithThreshold(calculator, taxRatesWithThreshold);
            }
            else if (TaxRate == "N")
            {
                foreach (var rate in taxRatesNoThreshold)
                {
                    if (GrossPay >= rate.WeeklyEarningsLower && GrossPay <= rate.WeeklyEarningsUpper)
                    {
                        rateA = rate.RateA;
                        rateB = rate.RateB;
                    }
                }

                var calculator = new PayCalculatorNoThreshold(GrossPay, rateA, rateB, TotalPay);
                CalculatePayrollNoThreshold(calculator, taxRatesNoThreshold);
            }
        }

        // Method to calculate payroll details with a tax threshold
        private void CalculatePayrollWithThreshold(PayCalculatorWithThreshold calculator, List<TaxRateWithThreshold> taxRatesWithThreshold)
        {
            // Net wage, tax, and superannuation calculations
            NetPay = calculator.CalculatePay();
            TaxAmount = calculator.CalculateTax();
            Superannuation = calculator.CalculateSuperannuation();
            GrossPay = TotalPay - TaxAmount + Superannuation;
            taxPerHour = TaxAmount / HoursWorked;
        }

        // Method to calculate payroll details without a tax threshold
        private void CalculatePayrollNoThreshold(PayCalculatorNoThreshold calculator, List<TaxRateNoThreshold> taxRatesNoThreshold)
        {
            // Net wage, tax, and superannuation calculations
            NetPay = calculator.CalculatePay();
            TaxAmount = calculator.CalculateTax();
            Superannuation = calculator.CalculateSuperannuation();
            GrossPay = TotalPay - TaxAmount + Superannuation;
            taxPerHour = TaxAmount / HoursWorked;
        }
    }
}
