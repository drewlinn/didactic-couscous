using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using HairSalon.Objects;

namespace HairSalon
{
  [Collection("HairSalon")]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=HairSalon_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseInitiallyEmpty()
    {
      int result = Stylist.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_StylistToDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Princess Carolyn", "101-0478-4710", "AgentPrincess@BojackHorseman.com");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
