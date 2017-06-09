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
