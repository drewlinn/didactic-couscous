using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using HairSalon.Objects;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>{
        return View["index.cshtml"];
      };
      Get["/clients"] = _ =>{
        List<Client> AllClients = Client.GetAll();
        return View["clients.cshtml", AllClients];
      };
      Get["/stylists"] = _ => {
        List<Stylist> AllStylist = Stylist.GetAll();
        return View["stylists.cshtml", AllStylist];
      };
      Get["/client/new"] = _ =>  {
        List<Stylist> AllStylist = Stylist.GetAll();
        return View["/add_client.cshtml", AllStylist];
      };
      Get["/stylist/new"] = _ =>  {
        return View["/add_stylist.cshtml"];
      };
      Post["/clients"]= _ =>{
        Dictionary<string, object> model = new Dictionary<string, object>();
        Client newClient = new Client(Request.Form["client-name"], Request.Form["client-phone"], Request.Form["client-email"], Request.Form["stylist_id"]);
        var selectedStylist = Stylist.Find(newClient.GetStylistId());
        newClient.Save();
        model.Add("stylist", selectedStylist);
        model.Add("client", newClient);
        return View["successfully_added.cshtml", model];
      };
      Post["/stylists"]= _ =>{
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-phone"], Request.Form["stylist-email"]);
        var selectedStylist = Stylist.Find(newStylist.GetId());
        newStylist.Save();
        model.Add("stylist", newStylist);
        return View["successfully_added.cshtml", newStylist];
      };
    }
  }
}
