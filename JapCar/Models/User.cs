using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Design;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapCar.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { set; get; }


        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual LoginHistory LoginHistorys { get; set; }
    }
}
