using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;

namespace github_issues_via_feedback_form.Controllers
{
  public class SimpleController : Controller
  {
    private RestRequest _restRequest;
    private string _username;
    private string _password;
    private string _resource;
    private string _defaultlabel;

    //
    // GET: /home/
    //
    public ActionResult Index()
    {
      return View();
    }

    //
    // POST: /sendfeedback/
    //
    [HttpPost]
    [ValidateInput(false)]
    public ActionResult sendfeedback(FormCollection form)
    {
      if (form["feedbackmessage"] == String.Empty) { return new EmptyResult(); }

      string html = "";
      html += "<p>Feedback: " + form["feedbackmessage"] + "</p>";
      html += "<p>URL: <a href='" + form["url"] + "'>" + form["url"] + "</a></p>";
      html += "<p>Type: " + Request.Browser.Type + "<br />";
      html += "Name: " + Request.Browser.Browser + "<br />";
      html += "Version: " + Request.Browser.Version + "<br />";
      html += "Major Version: " + Request.Browser.MajorVersion + "<br />";
      html += "Minor Version: " + Request.Browser.MinorVersion + "<br />";
      html += "Platform: " + Request.Browser.Platform + "<br />";
      html += "Is Beta: " + Request.Browser.Beta + "<br />";
      html += "Is Crawler: " + Request.Browser.Crawler + "<br />";
      html += "Is AOL: " + Request.Browser.AOL + "<br />";
      html += "Is Win16: " + Request.Browser.Win16 + "<br />";
      html += "Is Win32: " + Request.Browser.Win32 + "<br />";
      html += "Supports Frames: " + Request.Browser.Frames + "<br />";
      html += "Supports Tables: " + Request.Browser.Tables + "<br />";
      html += "Supports VB Script: " + Request.Browser.VBScript + "<br />";
      html += "Supports Javascript: " + Request.Browser.EcmaScriptVersion.ToString() + "<br />";
      html += "Supports JAVA Applets: " + Request.Browser.JavaApplets + "<br />";
      html += "CDF: " + Request.Browser.CDF + "</p>";

      //send feedback to github
      try
      {

      _username = System.Configuration.ConfigurationManager.AppSettings["username"];
      _password = System.Configuration.ConfigurationManager.AppSettings["password"];
      _resource = System.Configuration.ConfigurationManager.AppSettings["resource"];
      _defaultlabel = System.Configuration.ConfigurationManager.AppSettings["defaultlabel"];

        var client = new RestClient
        {
          BaseUrl = "https://api.github.com",
          Authenticator = new HttpBasicAuthenticator(_username, _password)
        };

        _restRequest = new RestRequest(Method.POST)
        {
          Resource = _resource,
          RequestFormat = DataFormat.Json
        };
        _restRequest.AddBody(new { title = form["feedbackmessage"], body = html, labels = new List<string> { _defaultlabel } });

        var response = client.Execute(_restRequest);
        var keyresponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

        string html_url = keyresponse.html_url; 

        return Json(new { status = response.StatusCode, message = response.Content, html_url = html_url });
      }
      catch (Exception ex) {
        return Json(new { data = "An MVC error occurred: " + ex.Message });
      }

    }
  }
}
