using System;
using RestSharp;
using github.Resources;

namespace github
{
  public class githubAPI
  {
    const string BaseUrl = "https://api.github.com";

    public readonly IssueResource Issues;

    public githubAPI(string username, string password)
    {
      RestClient client = new RestClient(BaseUrl);
      client.Authenticator = new HttpBasicAuthenticator(username, password);

      Issues = new IssueResource(client);
    }
  }
}