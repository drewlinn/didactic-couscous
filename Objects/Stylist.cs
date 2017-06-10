using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private string _phone;
    private string _email;

    public Stylist(string name, string phone, string email, int id = 0)
    {
      _id = id;
      _name = name;
      _phone = phone;
      _email = email;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        bool phoneEquality = (this.GetPhone() == newStylist.GetPhone());
        bool emailEquality = (this.GetEmail() == newStylist.GetEmail());
        return (idEquality && nameEquality && phoneEquality && emailEquality);
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

    public static List<Stylist> GetAll()
    {
      List<Stylist> AllStylists = new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM Stylists;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistPhone = rdr.GetString(2);
        string stylistEmail = rdr.GetString(3);
        Stylist newStylist = new Stylist(stylistName, stylistPhone, stylistEmail, stylistId);
        AllStylists.Add(newStylist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllStylists;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO Stylists (name, phone#, email) OUTPUT INSERTED.id VALUES (@stylistName, @stylistPhone, @stylistEmail);", conn);

      SqlParameter nameParam = new SqlParameter("@stylistName", this.GetName());
      SqlParameter phoneParam = new SqlParameter("@stylistPhone", this.GetPhone());
      SqlParameter emailParam = new SqlParameter("@stylistEmail", this.GetEmail());
      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(phoneParam);
      cmd.Parameters.Add(emailParam);
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

    public void Update(string newName, string newPhone, string newEmail)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE Stylists SET name = @newName, phone# = @newPhone, email = @newEmail OUTPUT INSERTED.name, INSERTED.phone#, INSERTED.email WHERE id = @id", conn);

      SqlParameter newNamePara = new SqlParameter("@newName", newName);
      SqlParameter newPhonePara = new SqlParameter("@newPhone", newPhone);
      SqlParameter newEmailPara = new SqlParameter("@newEmail", newEmail);
      SqlParameter idPara = new SqlParameter("@id", this.GetId());


      cmd.Parameters.Add(newNamePara);
      cmd.Parameters.Add(newPhonePara);
      cmd.Parameters.Add(newEmailPara);
      cmd.Parameters.Add(idPara);

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
          this._name = rdr.GetString(0);
          this._phone = rdr.GetString(1);
          this._email = rdr.GetString(2);
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

      SqlCommand cmd = new SqlCommand("DELETE FROM Stylists WHERE id = @StylistId; DELETE FROM Clients WHERE stylistId = @StylistId;", conn);

      SqlParameter stylistIdParam = new SqlParameter("@StylistId", this.GetId());

      cmd.Parameters.Add(stylistIdParam);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM Stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
