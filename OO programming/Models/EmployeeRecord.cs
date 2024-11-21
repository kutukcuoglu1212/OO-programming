using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming.Models
{
    // Class representing an Employee Record for payment details
    public class EmployeeRecord
    {
        // Properties to store payment details
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int HourlyRate { get; set; }
        public string TaxRate { get; set; }
        public int HoursWorked { get; set; }
        public float TotalPay { get; set; }
        public float GrossPay { get; set; }
        public float TaxAmount { get; set; }
        public float NetPay { get; set; }
        public float Superannuation { get; set; }
        public float TaxPerHour { get; set; }

    }
}
