using System.Data.SqlClient;
using System.Collections.Generic;
using System;

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

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }
    // public void SetId(int newId)
    // {
    //   _id = newId;
    // }
    public string GetName()
    {
      return _name;
    }
    // public void SetName(string newName)
    // {
    //   _name = newName;
    // }
    public string GetPhone()
    {
      return _phone;
    }
    // public void SetPhone(string newPhone)
    // {
    //   _phone = newPhone;
    // }
    public string GetEmail()
    {
      return _email;
    }
    // public void SetEmail(string newEmail)
    // {
    //   _email = newEmail;
    // }
    public int GetStylistId()
    {
      return _stylist_id;
    }
    // public void SetStylistId(int stylistId)
    // {
    //   _stylist = newStylistId
    // }

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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO Clients (name, phone#, email, stylistId) OUTPUT INSERTED.id VALUES (@name, @phone, @email, @stylistId);", conn);

      SqlParameter nameParam = new SqlParameter("@name", this.GetName());

      SqlParameter phoneParam = new SqlParameter("@phone", this.GetPhone());

      SqlParameter emailParam = new SqlParameter("@email", this.GetEmail());

      SqlParameter stylistIdParam = new SqlParameter("@stylistId", this.GetStylistId());

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(phoneParam);
      cmd.Parameters.Add(emailParam);
      cmd.Parameters.Add(stylistIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string newName, string newPhone, string newEmail, int newStylistId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE Clients SET name = @newName, phone# = @newPhone, email = @newEmail, stylistId = @newStylistId OUTPUT INSERTED.name, INSERTED.phone#, INSERTED.email, INSERTED.stylistId WHERE id = @id", conn);

      SqlParameter newNamePara = new SqlParameter("@newName", newName);
      SqlParameter newPhonePara = new SqlParameter("@newPhone", newPhone);
      SqlParameter newEmailPara = new SqlParameter("@newEmail", newEmail);
      SqlParameter newStylistIdPara = new SqlParameter("@newStylistId", newStylistId);
      SqlParameter idPara = new SqlParameter("@id", this.GetId());


      cmd.Parameters.Add(newNamePara);
      cmd.Parameters.Add(newPhonePara);
      cmd.Parameters.Add(newEmailPara);
      cmd.Parameters.Add(newStylistIdPara);
      cmd.Parameters.Add(idPara);

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
          this._name = rdr.GetString(0);
          this._phone = rdr.GetString(1);
          this._email = rdr.GetString(2);
          this._stylist_id = rdr.GetInt32(3);
      }
      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM Clients WHERE id = @id;", conn);

      SqlParameter clientIdParam = new SqlParameter("@id", this.GetId());

      cmd.Parameters.Add(clientIdParam);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM Clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM Clients WHERE id = @id;", conn);
      SqlParameter idParameter = new SqlParameter("@id", id.ToString());

      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string name = null;
      string phone = null;
      string email = null;
      int stylist_id = 0;
      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        phone = rdr.GetString(2);
        email = rdr.GetString(3);
        stylist_id = rdr.GetInt32(4);
      }
      Client foundClient = new Client(name, phone, email, stylist_id, foundId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundClient;
    }
  }
}
