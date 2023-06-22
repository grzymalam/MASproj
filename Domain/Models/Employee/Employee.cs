using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models.Employees
{
    public abstract class Employee : IPerson
    {
        protected Employee(string pesel, string name, string lastname, double hourlyWage, uint hoursWorked, DateTime employedDate, Location location)
        {
            Id = Guid.NewGuid();
            HourlyWage = hourlyWage;
            HoursWorked = hoursWorked;
            EmployedDate = employedDate;
            Pesel = pesel;
            Name = name;
            Lastname = lastname;
            Location = location;
        }
        protected Employee() { }
        public double HourlyWage { get; set; }
        public uint HoursWorked { get; set; }
        public DateTime EmployedDate { get; set; }
        public Guid Id { get; set; }
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Location Location { get; set; }

        public abstract double CalculateCurrentSalary();
    }
}
