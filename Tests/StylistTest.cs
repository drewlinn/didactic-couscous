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
      Stylist testStylist = new Stylist("Diane Nguyen", "567-890-1234", "GhostWriter@BojackHorseman.com");
      testStylist.Save();
      string newPhone = "789-012-3456";
      testStylist.Update("Diane Nguyen", "789-012-3456", "GhostWriter@BojackHorseman.com");
      string result = testStylist.GetPhone();
      Assert.Equal(newPhone, result);
    }

    [Fact]
    public void Test_Delete_StylistFromDatabase()
    {
      Stylist testStylist1 = new Stylist("Bojack", "123-456-7890", "Secretariat@BojackHorseman.com");
      testStylist1.Save();
      Stylist testStylist2 = new Stylist("Todd", "098-765-4321", "RockOperaMaestro@BojackHorseman.com");
      testStylist2.Save();
      Client testClient = new Client("Sleepy Gary", "123-450-9877", "Boatfan@memoryparasite.com", testStylist1.GetId());
      testClient.Save();
      Client testClient1 = new Client("Mr. Beauregard", "555-867-5309", "LoyalButler@memoryparasite.com", testStylist2.GetId());
      testClient1.Save();
      testStylist1.Delete();
      List<Stylist> resultStylist = Stylist.GetAll();
      List<Stylist> testStylist = new List<Stylist> {testStylist2};
      List<Client> resultClients = Client.GetAll();
      List<Client> testClientList = new List<Client> {testClient1};
      Assert.Equal(testStylist, resultStylist);
      Assert.Equal(testClientList, resultClients);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}
