using NUnit.Framework;
using OO_programming.Payroll;
using OO_programming.Tax;
using System;
using System.Collections.Generic;

namespace OO_programming.Tests
{
    [TestFixture]
    public class PaySlipTests
    {
        [Test]
        public void TestCalculatePayrollWithThreshold()
        {
            // Arrange
            // Employee's hourly rate and hours worked
            float hourlyRate = 25.0f;
            float hoursWorked = 30.0f;
            // Tax rate (Y: with threshold)
            string taxRate = "Y";
            // Rates used for tax calculation
            float RateA = 0.219f;
            float RateB = 74.8369f;
            // List of threshold rates for tax calculations
            List<TaxRateWithThreshold> taxRatesWithThreshold = new List<TaxRateWithThreshold>
            {
                new TaxRateWithThreshold { WeeklyEarningsLower = 548, WeeklyEarningsUpper = 721, RateA = RateA, RateB = RateB }
            };
            // List of non-threshold rates for tax calculations
            List<TaxRateNoThreshold> taxRatesNoThreshold = new List<TaxRateNoThreshold>();

            // Act
            // Operations including tax calculations are performed when creating the PaySlip
            var paySlip = new PaySlip(hourlyRate, hoursWorked, taxRate, taxRatesWithThreshold, taxRatesNoThreshold);

            // Assert
            // Check if the total pay is calculated correctly
            Assert.AreEqual(750.0f, paySlip.TotalPay); //TotalPay
            // Check if the tax amount is calculated correctly
            var taxAmount = (((paySlip.TotalPay + 0.99f) * RateA)) - RateB; // TaxAmount
            Assert.AreEqual(89.6299133f, taxAmount);  // TaxAmount
            // Check if the gross pay is calculated correctly
            Assert.AreEqual(746.620117f, paySlip.TotalPay - taxAmount + paySlip.Superannuation);  // GrossPay
            // Check if the Superannuation amount is calculated correctly
            Assert.AreEqual(86.25f, paySlip.Superannuation); //Superannuation
            // Check if the tax amount per hour is calculated correctly
            Assert.AreEqual(2.98766375f, taxAmount / hoursWorked); //TaxPerHour
        }

        [Test]
        public void TestCalculatePayrollNoThreshold()
        {
            // Arrange
            // Employee's hourly rate and hours worked
            float hourlyRate = 25.0f;
            float hoursWorked = 30.0f;
            // Tax rate (N: no threshold)
            string taxRate = "N";
            // Rates used for tax calculation
            float RateA = 0.3477f;
            float RateB = 64.4297f;
            // List of threshold rates for tax calculations
            List<TaxRateWithThreshold> taxRatesWithThreshold = new List<TaxRateWithThreshold>();
            // List of non-threshold rates for tax calculations
            List<TaxRateNoThreshold> taxRatesNoThreshold = new List<TaxRateNoThreshold>
            {
                new TaxRateNoThreshold { WeeklyEarningsLower = 515, WeeklyEarningsUpper = 932, RateA = RateA, RateB = RateB }
            };

            // Act
            // Operations including tax calculations are performed when creating the PaySlip
            var paySlip = new PaySlip(hourlyRate, hoursWorked, taxRate, taxRatesWithThreshold, taxRatesNoThreshold);

            // Assert
            // Check if the total pay is calculated correctly
            Assert.AreEqual(750.0f, paySlip.TotalPay); //TotalPay
            // Check if the tax amount is calculated correctly
            var taxAmount = (((paySlip.TotalPay + 0.99f) * RateA)) - RateB; // TaxAmount
            Assert.AreEqual(196.689529f, taxAmount);  // TaxAmount
            // Check if the gross pay is calculated correctly
            Assert.AreEqual(639.560486f, paySlip.TotalPay - taxAmount + paySlip.Superannuation);  // GrossPay
            // Check if the Superannuation amount is calculated correctly
            Assert.AreEqual(86.25f, paySlip.Superannuation); //Superannuation
            // Check if the tax amount per hour is calculated correctly
            Assert.AreEqual(6.55631781f, taxAmount / hoursWorked); //TaxPerHour
        }

        [Test]
        public void TestCalculatePayrollNoThresholdMinPay()
        {
            // Arrange
            // Employee's hourly rate and hours worked (minimum payment situation)
            float hourlyRate = 25.0f;
            float hoursWorked = 1.0f;
            // Tax rate (N: no threshold)
            string taxRate = "N";
            // Rates used for tax calculation
            float RateA = 0.19f;
            float RateB = 0.19f;
            // List of threshold rates for tax calculations
            List<TaxRateWithThreshold> taxRatesWithThreshold = new List<TaxRateWithThreshold>();
            // List of non-threshold rates for tax calculations
            List<TaxRateNoThreshold> taxRatesNoThreshold = new List<TaxRateNoThreshold>
            {
                new TaxRateNoThreshold { WeeklyEarningsLower = 0, WeeklyEarningsUpper = 88, RateA = RateA, RateB = RateB }
            };

            // Act
            // Operations including tax calculations are performed when creating the PaySlip
            var paySlip = new PaySlip(hourlyRate, hoursWorked, taxRate, taxRatesWithThreshold, taxRatesNoThreshold);

            // Assert
            // Check if the total pay is calculated correctly
            Assert.AreEqual(25.0f, paySlip.TotalPay); //TotalPay
            // Check if the tax amount is calculated correctly
            var taxAmount = (((paySlip.TotalPay + 0.99f) * RateA)) - RateB; // TaxAmount
            Assert.AreEqual(4.7481f, taxAmount);  // TaxAmount
            // Check if the gross pay is calculated correctly
            Assert.AreEqual(23.1268997f, paySlip.TotalPay - taxAmount + paySlip.Superannuation);  // GrossPay
            // Check if the Superannuation amount is calculated correctly
            Assert.AreEqual(2.875f, paySlip.Superannuation); //Superannuation
            // Check if the tax amount per hour is calculated correctly
            Assert.AreEqual(4.7481f, taxAmount / hoursWorked); //TaxPerHour
        }
    }
}
