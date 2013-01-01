using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;
using github;

namespace github_issues_via_feedback_form.Controllers
{
  public class HomeController : Controller
  {
    githubAPI api = new githubAPI(System.Configuration.ConfigurationManager.AppSettings["username"], System.Configuration.ConfigurationManager.AppSettings["password"]);
    private string _repo = "github-issues-via-feedback-form";

    //
    // GET: /home/
    //
    public ActionResult Index()
    {
      List<Issue> issues = api.Issues.List();

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

      string _defaultlabel = System.Configuration.ConfigurationManager.AppSettings["defaultlabel"];

      try
      {
        Issue issue = api.Issues.Create(new Dictionary<string, object>() {
          {"resource", "repos/" + System.Configuration.ConfigurationManager.AppSettings["username"] + "/" +_repo + "/issues"},
          {"addBody",  new { title = form["feedbackmessage"], body = html, labels = new List<string> { _defaultlabel } } }
        });

        return Json(new { status = "201", message = "Success", html_url = issue.html_url });
      }
      catch (githubException e)
      {
        System.Diagnostics.Debug.WriteLine(e.Message);
        return Json(new { data = "An error occurred: " + e.Message });
      }

    }
  }
}
