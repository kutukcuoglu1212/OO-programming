using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming.Models
{
    // Class representing an Employee
    public class Employee
    {
        // Properties to store employee information
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float HourlyRate { get; set; }
        public string TaxThreshold { get; set; }

        // Override ToString method to provide a formatted string representation
        public override string ToString()
        {
            return $"{EmployeeId} - {FirstName} - {LastName} - {HourlyRate} - {TaxThreshold}";
        }
    }
}
