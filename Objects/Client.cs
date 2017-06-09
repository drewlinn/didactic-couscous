using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
  public class Client
  {
    private int _id;
    private string _name;
    private string _phone;
    private string _email;
    private int _stylist_id;

    public Client(string name, string phone, string email, int stylistId, int id = 0)
    {
      _name = name;
      _phone = phone;
      _email = email;
      _stylist_id = stylistId;
      _id = id;
    }

    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool phoneEquality = (this.GetPhone() == newClient.GetPhone());
        bool emailEquality = (this.GetEmail() == newClient.GetEmail());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        return (idEquality && nameEquality && phoneEquality && emailEquality && stylistIdEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetPhone()
    {
      return _phone;
    }
    public string GetEmail()
    {
      return _email;
    }
    public int GetStylistId()
    {
      return _stylist_id;
    }

    public static List<Client> GetAll()
    {
      List<Client> AllClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM Clients", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string phone = rdr.GetString(2);
        string email = rdr.GetString(3);
        int stylistId = rdr.GetInt32(4);
        Client newClient = new Client(name, phone, email, stylistId, id);
        AllClients.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllClients;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM Clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }


  }
}
