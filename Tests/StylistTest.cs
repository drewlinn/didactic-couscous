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
      Stylist testStylist = new Stylist("Princess Carolyn", "101-0478-4710", "AgentPrincess@BojackHorseman.com");
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Update_StylistInDatabase()
    {
      //Arrange
      string phone = "456-789-1211";
      Stylist testStylist = new Stylist("Diane Nguyen", "567-890-1234", "GhostWriter@BojackHorseman.com");
      testStylist.Save();
      string newPhone = "789-012-3456";

      //Act
      testStylist.Update("Diane Nguyen", "789-012-3456", "GhostWriter@BojackHorseman.com");
      string result = testStylist.GetPhone();

      //Assert
      Assert.Equal(newPhone, result);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
