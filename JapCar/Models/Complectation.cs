using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapCar.Models
{
    public partial class Complectation
    {
        public Complectation()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
