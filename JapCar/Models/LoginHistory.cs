using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapCar.Models
{
    public partial class LoginHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginDate { set; get; }

        public virtual ICollection<User> Users { get; set; }

    }
}
