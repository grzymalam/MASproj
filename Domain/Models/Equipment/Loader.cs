using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Equipment
{
    public class Loader : PieceOfEquipment
    {
        public Loader(DateTime dateOfPurchase, double mileage, double pricePerDay,
            DateTime lastInspection, double mass, string name, LoaderType loaderType, double width) 
            : base(dateOfPurchase, mileage, pricePerDay, lastInspection, mass, name)
        {
            LoaderType = loaderType;
            Width = width;
        }

        public LoaderType LoaderType { get; set; }
        public double Width { get; set; }
        public override double CalculateEarnedMoney()
        {
            throw new NotImplementedException();
        }
    }
    public enum LoaderType
    {
        Tracked, Articulated
    }
}
