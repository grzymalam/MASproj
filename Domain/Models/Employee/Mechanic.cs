using Domain.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Employees
{
    public class Mechanic : Employee
    {
        public Mechanic(string pesel, string name, string lastname, double hourlyWage, uint hoursWorked, DateTime employedDate, ExperienceLevel experienceLevel, uint amountOfRepairedEquipmentPieces, Location location) : base(pesel, name, lastname, hourlyWage, hoursWorked, employedDate, location)
        {
            ExperienceLevel = experienceLevel;
            AmountOfRepairedEquipmentPieces = amountOfRepairedEquipmentPieces;
        }

        protected Mechanic() { }

        public ExperienceLevel ExperienceLevel { get; set; }
        public uint AmountOfRepairedEquipmentPieces { get; set; }
        public override double CalculateCurrentSalary()
        {
            return HoursWorked * HourlyWage + AmountOfRepairedEquipmentPieces * 100;
        }
        public static uint CalculateAmountOfRepairedPiecesOfEquipment(IEnumerable<Mechanic> mechanics)
        {
            return (uint)mechanics.Sum(m => m.AmountOfRepairedEquipmentPieces);
        }
    }
}
