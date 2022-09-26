using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JapCar.Models;

namespace JapCar.CustomModels
{
   public class CustomLoginHistory
    {
        public DateTime LoginDate { get; set; }
        public string RoleType { get; set; }
        public string UserLogin { get; set; }
    }
}
