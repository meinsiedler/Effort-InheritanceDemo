using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EffortInheritance.Domain;

namespace EffortInheritance.Data
{
  public class VehicleContext : DbContext
  {
    public VehicleContext(DbConnection connection) : base(connection, contextOwnsConnection:true)
    {
    }

    public VehicleContext() : base("DefaultConnection")
    {
    }

    public IDbSet<Person> Persons { get; set; }
    public IDbSet<Vehicle> Vehicles { get; set; } 
  }
}
