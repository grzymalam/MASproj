using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public interface IPerson
    {
        public Guid Id { get; set; }
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
