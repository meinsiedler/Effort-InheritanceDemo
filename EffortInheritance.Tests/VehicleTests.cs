using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Effort.DataLoaders;
using Effort.Extra;
using EffortInheritance.Domain;
using NUnit.Framework;

namespace EffortInheritance.Tests
{
  [TestFixture]
  public class VehicleTests
  {
    [Test]
    public void SelectPersons_WithObjectDataLoader()
    {
      var data = new ObjectData(TableNamingStrategy.Pluralised);
      data.Table<Person>().Add(new Person { Id = 1, FirstName = "Ned", LastName = "Flanders" });
      data.Table<Person>().Add(new Person { Id = 2, FirstName = "Lisa", LastName = "Simpson" });

      using (var context = EffortFactory.CreateVehicleContext(MethodBase.GetCurrentMethod().Name, new ObjectDataLoader(data)))
      {
        var persons = context.Persons.OrderBy(p => p.Id).ToList();
        Assert.That(persons.Count, Is.EqualTo(2));

        Assert.That(persons[0].FirstName, Is.EqualTo("Ned"));
        Assert.That(persons[0].LastName, Is.EqualTo("Flanders"));

        Assert.That(persons[1].FirstName, Is.EqualTo("Lisa"));
        Assert.That(persons[1].LastName, Is.EqualTo("Simpson"));
      }
    }

    /// <summary>
    /// Note that this test currently fails, because it seems like Effort.Extra doesn't support inheritance.
    /// See https://github.com/christophano/Effort.Extra/issues/8 for details.
    /// </summary>
    [Test]
    public void SelectCars_WithObjectDataLoader()
    {
      var data = new ObjectData(TableNamingStrategy.Pluralised);
      data.Table<Vehicle>().Add(new Car { Id = 1, Name = "Volkswagen Beetle", Power = 34 });
      data.Table<Vehicle>().Add(new Car { Id = 2, Name = "Citroen 2CV", Power = 22 });

      using (var context = EffortFactory.CreateVehicleContext(MethodBase.GetCurrentMethod().Name, new ObjectDataLoader(data)))
      {
        var cars = context.Vehicles.OfType<Car>().OrderBy(p => p.Id).ToList();
        Assert.That(cars.Count, Is.EqualTo(2));
      }
    }

    [Test]
    public void SelectPersons_WithCsvDataLoader()
    {
      var csvDataLoader = new CsvDataLoader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataFiles"));
      using (var context = EffortFactory.CreateVehicleContext(MethodBase.GetCurrentMethod().Name, csvDataLoader))
      {
        var persons = context.Persons.OrderBy(p => p.Id).ToList();
        Assert.That(persons.Count, Is.EqualTo(2));
      }
    }

    [Test]
    public void SelectCars_WithCsvDataLoader()
    {
      var csvDataLoader = new CsvDataLoader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataFiles"));
      using (var context = EffortFactory.CreateVehicleContext(MethodBase.GetCurrentMethod().Name, csvDataLoader))
      {
        var cars = context.Vehicles.OfType<Car>().OrderBy(p => p.Id).ToList();
        Assert.That(cars.Count, Is.EqualTo(2));
      }
    }
  }
}
