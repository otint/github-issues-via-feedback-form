﻿using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Deserializers;

namespace github
{
  public class githubValidationException : Exception
  {
    public readonly Dictionary<string, List<string>> Errors;

    private string message;

    public IRestResponse Response;

    public githubValidationException(IRestResponse response)
    {
      JsonDeserializer deserializer = new JsonDeserializer();
      Console.WriteLine(response.Content);
      Errors = deserializer.Deserialize<Dictionary<string, List<string>>>(response);

      message = "Invalid model: ";
      foreach (KeyValuePair<string, List<string>> field in Errors)
      {
        message += field.Key + " " + string.Join("and", field.Value.ToArray());
      }
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