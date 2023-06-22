using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Employees
{
    public class Salesman : Employee
    {
        public Salesman(string pesel, string name, string lastname, double hourlyWage, uint hoursWorked, DateTime employedDate, uint amountOfRentedPiecesOfEquipment, Location location) : base(pesel, name, lastname, hourlyWage, hoursWorked, employedDate, location)
        {
            AmountOfRentedPiecesOfEquipment = amountOfRentedPiecesOfEquipment;
        }
        protected Salesman() { }
        public uint AmountOfRentedPiecesOfEquipment { get; set; }
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public override double CalculateCurrentSalary()
        {
            return HourlyWage * HoursWorked + AmountOfRentedPiecesOfEquipment * 150;
        }
    }
}
