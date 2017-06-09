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
      //Arrange, Act
      int result = Client.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      Client.DeleteAll();
      // Stylist.DeleteAll();
    }
  }
}
