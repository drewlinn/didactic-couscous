using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using HairSalon.Objects;

namespace HairSalon
{
  [Collection("HairSalon")]
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=HairSalon_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseInitiallyEmpty()
    {
      int result = Client.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_ClientToDatabase()
    {
      Client testClient = new Client("Rick Sanchez", "123-456-7890", "Rick.Sanchez@RickandMorty.com", 1);

      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Update_ClientInDatabase()
    {
      string name = "Morty Smith";
      string phone = "555-555-5555";
      string email = "SecretlyACar@RickandMorty.com";
      Client testClient = new Client(name, phone, email, 1);
      testClient.Save();
      string newName = "EvilMorty";
      string newPhone = "555-545-5555";
      string newEmail = "EvilMorty@RickandMorty.com";
      testClient.Update(newName, newPhone, newEmail);
      string result = testClient.GetPhone();
      Assert.Equal(newPhone, result);
    }

    [Fact]
    public void Test_Delete_ClientFromDatabase()
    {
      //Arrange
      Client testClient1 = new Client("Jerry Smith", "789-101-1121", "HungryForApples@RickandMorty.com", 1);
      testClient1.Save();
      Client testClient2 = new Client("Rick Sanchez", "121-028-0641", "PortalGunFun@RickandMorty", 2);
      testClient2.Save();

      //Act
      testClient1.Delete();
      List<Client> resultClients = Client.GetAll();
      List<Client> testClient = new List<Client> {testClient2};
      //Assert
      Assert.Equal(resultClients, testClient);

    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}