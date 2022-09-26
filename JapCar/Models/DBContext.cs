using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapCar.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("DbConnection")
        { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
        public virtual DbSet<Complectation> Complectations { get; set; }
    }
}
