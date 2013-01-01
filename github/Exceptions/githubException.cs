using System;
using RestSharp;

namespace github
{
  public class githubException : Exception
  {
    private string message;

    public IRestResponse Response;

    public githubException(IRestResponse response)
    {
      Response = response;
      message = "Unexpected response status " + ((int)response.StatusCode).ToString() + " with body:\n" + response.Content;
    }

    public override string Message
    {
      get
      {
        return message;
      }
    }
  }
}