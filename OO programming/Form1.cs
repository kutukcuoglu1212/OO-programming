using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using OO_programming.Models;
using OO_programming.Payroll;
using OO_programming.Tax;

namespace OO_programming
{
    public partial class Form1 : Form
    {
        // Lists to store tax rates
        private List<TaxRateWithThreshold> taxRatesWithThreshold;
        private List<TaxRateNoThreshold> taxRatesNoThreshold;

        public Form1()
        {
            InitializeComponent();
            LoadTaxRateFiles();
            LoadEmployeeList();
        }

        // Method triggered when the "Calculate" button is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            // Check if any item is selected in the listbox
            if (listBoxEmployee.SelectedItem == null)
            {
                MessageBox.Show("Please select an employee before calculating.");
                return;
            }
            //int control = Convert.ToInt32(txtHoursWorked.Text);
            if (txtHoursWorked.Text == null || txtHoursWorked.Text == "" || txtHoursWorked.Text == "0")
            {
                MessageBox.Show("Please fill in the Hours Worked information.");
                return;
            }

            // Get the hours worked from the user input
            if (!int.TryParse(txtHoursWorked.Text, out int hoursWorked) || hoursWorked < 0)
            {
                MessageBox.Show("Please enter a valid numeric value for hours worked.");
                return;
            }

            

            // Get the selected employee from the listbox
            string selectedText = listBoxEmployee.SelectedItem.ToString();
            string[] values = selectedText.Split('-');

            // Extract necessary information
            int id = int.Parse(values[0].Trim());
            string name = values[1].Trim();
            string lastname = values[2].Trim();
            int hourlyRate = int.Parse(values[3].Trim());
            string taxRate = values[4].Trim();

            // Get the hours worked from the user input
            hoursWorked = int.Parse(txtHoursWorked.Text);

            // Create a PaySlip object
            var paySlip = new PaySlip(hourlyRate, hoursWorked, taxRate, taxRatesWithThreshold, taxRatesNoThreshold);

            // Display the payment summary
            txtPaymentSummary.Text = $"Employee ID: {id}\r\n" +
                $"Employee First Name: {name}\r\n" +
                $"Employee Last Name: {lastname}\r\n" +
                $"Hours Worked: {paySlip.HoursWorked}\r\n" +
                $"Hourly Rate: {paySlip.HourlyRate}\r\n" +
                $"Tax Threshold: {paySlip.TaxRate}\r\n" +
                $"Total Pay: {paySlip.TotalPay}\r\n" +
                $"Gross Pay: {paySlip.GrossPay}\r\n" +
                            $"Tax: {paySlip.TaxAmount}\r\n" +
                            $"Net Pay: {paySlip.NetPay}\r\n" +
                            $"Superannuation: {paySlip.Superannuation}\r\n" +
                            $"Tax Per Hour: {paySlip.taxPerHour}\r\n";
        }

        // Method triggered when the "Save" button is clicked
        private void button2_Click(object sender, EventArgs e)
        {
            // Check if any item is selected in the listbox
            if (listBoxEmployee.SelectedItem == null)
            {
                MessageBox.Show("Please select an employee before saving.");
                return;
            }

            // Get the selected employee from the listbox
            string selectedText = listBoxEmployee.SelectedItem.ToString();
            string[] values = selectedText.Split('-');

            // Extract necessary information
            int id = int.Parse(values[0].Trim());
            string name = values[1].Trim();
            string lastName = values[2].Trim();
            int hourlyRate = int.Parse(values[3].Trim());
            string taxRate = values[4].Trim();
           

            // Get the hours worked from the user input
            int hoursWorked = int.Parse(txtHoursWorked.Text);

            // Get the payment summary
            string paymentSummaryText = txtPaymentSummary.Text;

            // Parse the payment summary
            float totalPay = 0.0f;
            float grossPay = 0.0f;
            float taxAmount = 0.0f;
            float netPay = 0.0f;
            float superannuation = 0.0f;
            float taxPerHour = 0.0f;

            ParsePaymentSummary(paymentSummaryText,ref totalPay, ref grossPay, ref taxAmount, ref netPay, ref superannuation, ref taxPerHour);

            // Create a file name
            string fileName = $"Pay-EmployeeID-{id}-Fullname-{name+"-"+lastName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            try
            {
                SavePaymentDataToCsv(filePath, id, name, lastName, hourlyRate, taxRate, hoursWorked, totalPay, grossPay, taxAmount, netPay, superannuation,taxPerHour);
                MessageBox.Show("Payment data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving payment data: {ex.Message}");
            }
        }

        // Method to save data to a CSV file
        public void SaveToCsv(string filePath, IEnumerable<EmployeeRecord> records)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

        // Method to load tax rate files
        private void LoadTaxRateFiles()
        {
            try
            {
                // Read tax rate files and populate the lists
                taxRatesWithThreshold = LoadTaxRates<TaxRateWithThreshold>("taxrate-withthreshold.csv");
                taxRatesNoThreshold = LoadTaxRates<TaxRateNoThreshold>("taxrate-nothreshold.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tax rate files: {ex.Message}");
            }
        }

        // General method to read a CSV file
        private List<T> LoadTaxRates<T>(string filePath) where T : class
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false, Delimiter = "," }))
            {
                return csv.GetRecords<T>().ToList();
            }
        }

        // Method to load the employee list
        private void LoadEmployeeList()
        {
            // Read the employee list and bind it to the listbox
            using (var reader = new StreamReader("employee.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false, Delimiter = "," }))
            {
                var employees = csv.GetRecords<Employee>().ToList();

                listBoxEmployee.DataSource = employees;
                listBoxEmployee.DisplayMember = "FullName";
            }
        }

        // Method to parse the payment summary
        private void ParsePaymentSummary(string paymentSummaryText,ref float totalPay, ref float grossPay, ref float taxAmount, ref float netPay, ref float superannuation,ref float taxPerHour)
        {
            // Parse the payment summary
            string[] lines = paymentSummaryText.Split(new[] { "\r\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string propertyName = parts[0].Trim();
                    string propertyValue = parts[1].Trim();

                    // Assign values to the respective variables based on propertyName
                    switch (propertyName)
                    {
                        case "Total Pay":
                            float.TryParse(propertyValue, out totalPay);
                            break;

                        case "Gross Pay":
                            float.TryParse(propertyValue, out grossPay);
                            break;

                        case "Tax":
                            float.TryParse(propertyValue, out taxAmount);
                            break;

                        case "Net Pay":
                            float.TryParse(propertyValue, out netPay);
                            break;

                        case "Superannuation":
                            float.TryParse(propertyValue, out superannuation);
                            break;
                        case "Tax Per Hour":
                            float.TryParse(propertyValue, out taxPerHour);
                            break;
                    }
                }
            }
        }

        // Method to save payment data to a CSV file
        private void SavePaymentDataToCsv(string filePath, int id, string name, string lastname, int hourlyRate, string taxRate, int hoursWorked, float totalPay,float grossPay, float taxAmount, float netPay, float superannuation,float taxPerHour)
        {
            // Write employee data to a CSV file
            var records = new List<EmployeeRecord>
            {
                new EmployeeRecord
                {
                    EmployeeId = id,
                    Name = name,
                    LastName = lastname,
                    HourlyRate = hourlyRate,
                    TaxRate = taxRate,
                    HoursWorked = hoursWorked,
                    TotalPay = totalPay,
                    GrossPay = grossPay,
                    TaxAmount = taxAmount,
                    NetPay = netPay,
                    Superannuation = superannuation,
                    TaxPerHour = taxPerHour
                    
                   
                }
            };

            SaveToCsv(filePath, records);
        }
    }
}

