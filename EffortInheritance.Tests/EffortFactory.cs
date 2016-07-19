using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effort.DataLoaders;
using EffortInheritance.Data;

namespace EffortInheritance.Tests
{
  public class EffortFactory
  {
    public static VehicleContext CreateVehicleContext(string instanceId, IDataLoader dataLoader)
    {
      var connection = Effort.DbConnectionFactory.CreatePersistent(instanceId, dataLoader);
      return new VehicleContext(connection);
    } 
  }
}
