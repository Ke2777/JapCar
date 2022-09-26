using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapCar.Models
{
    public partial class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int ColorId { get; set; }
        public int ComplectationId { get; set; }
        public decimal Price { get; set; }
        public int CreateDate { get; set; }
        

        public virtual Color Colors { get; set; }
        [ForeignKey("ComplectationId")]
        public virtual Complectation Complectation { get; set; }
    }
    
}
