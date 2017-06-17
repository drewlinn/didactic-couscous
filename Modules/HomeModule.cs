using System;
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
      //CREATE
      Get["/client/new"] = _ =>  {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["add_client.cshtml", AllStylists];
      };
      Get["/stylist/new"] = _ =>  {
        return View["add_stylist.cshtml"];
      };
      Post["/clients"]= _ =>{
        Dictionary<string, object> model = new Dictionary<string, object>();
        Client newClient = new Client(Request.Form["client-name"], Request.Form["client-phone"], Request.Form["client-email"], Request.Form["stylist"]);
        var selectedStylist = Stylist.Find(newClient.GetStylistId());
        newClient.Save();
        model.Add("stylist", selectedStylist);
        model.Add("client", newClient);
        return View["success.cshtml", model];
      };
      Post["/stylists"]= _ =>{
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-phone"], Request.Form["stylist-email"]);
        var selectedStylist = Stylist.Find(newStylist.GetId());
        newStylist.Save();
        model.Add("stylist", newStylist);
        return View["success.cshtml", newStylist];
      };
      //READ
      Get["/clients"] = _ =>{
        List<Client> AllClients = Client.GetAll();
        return View["clients.cshtml", AllClients];
      };
      Get["/stylists"] = _ => {
        List<Stylist> AllStylist = Stylist.GetAll();
        return View["stylists.cshtml", AllStylist];
      };
      Get["/clients/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedClient = Client.Find(parameters.id);
        var selectedStylist = Stylist.Find(selectedClient.GetStylistId());
        model.Add("stylist", selectedStylist);
        model.Add("client", selectedClient);
        return View["client.cshtml", model];
      };
      Get["/stylists/{id}"]= parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedStylist = Stylist.Find(parameters.id);
        var ClientsStylist = selectedStylist.GetClients();
        model.Add("stylist", selectedStylist);
        model.Add("clients", ClientsStylist);
        return View["stylist.cshtml", model];
      };
      //UPDATE
      Get["/client/edit/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Client SelectedClient = Client.Find(parameters.id);
        List<Stylist> AllStylists = Stylist.GetAll();
        model.Add("client", SelectedClient);
        model.Add("stylists", Stylist.GetAll());
        return View["edit_client.cshtml", model];
      };
      Get["/stylist/edit/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["edit_stylist.cshtml", SelectedStylist];
      };
      Patch["/client/edit/{id}"] = parameters =>{
        Client SelectedClient = Client.Find(parameters.id);
        var selectedStylist = Stylist.Find(SelectedClient.GetStylistId());
        SelectedClient.Update(Request.Form["client-name"], Request.Form["client-phone"], Request.Form["client-email"], Request.Form["stylist"]);
        return View["success.cshtml"];
      };
      Patch["/stylist/edit/{id}"] = parameters =>{
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Update(Request.Form["stylist-name"], Request.Form["stylist-phone"], Request.Form["stylist-email"]);
        return View["success.cshtml"];
      };
      //DESTROY
      Get["client/delete/{id}"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        return View["delete_client.cshtml", SelectedClient];
      };
      Get["stylist/delete/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["delete_stylist.cshtml", SelectedStylist];
      };
      Delete["client/delete/{id}"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        SelectedClient.Delete();
        return View["success.cshtml"];
      };
      Delete["stylist/delete/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Delete();
        return View["success.cshtml"];
      };
      Post["/clients/cleared"] = _ =>{
        Client.DeleteAll();
        return View["cleared.cshtml"];
      };
      Post["/stylists/cleared"] = _ =>{
        Client.DeleteAll();
        Stylist.DeleteAll();
        return View["cleared.cshtml"];
      };
    }
  }
}
