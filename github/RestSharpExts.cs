using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace github
{
  public static class RestSharpExts
  {
    /// <summary>
    /// Assembles URL to call based on parameters, method and resource
    /// </summary>
    /// <param name="client">RestClient performing the execution</param>
    /// <param name="request">RestRequest to execute</param>
    /// <returns>Assembled System.Uri</returns>
    /// <remarks>
    /// RestClient's BuildUri purposefully leaves off the parameters from the uri it builds when the request 
    /// is a POST, PUT, or PATCH.  This extension method aims to undo this feature for debugging reasons
    /// </remarks>
    public static Uri BuildDebugUri(this IRestClient client, IRestRequest request)
        {
            var uri = client.BuildUri(request);
            if (request.Method != Method.POST &&
                request.Method != Method.PUT &&
                request.Method != Method.PATCH)
            {
                return uri;
            }
            else
            {
                var queryParameters = from p in request.Parameters
                                      where p.Type == ParameterType.GetOrPost
                                      select string.Format("{0}={1}", Uri.EscapeDataString(p.Name), Uri.EscapeDataString(p.Value.ToString()));
                if (!queryParameters.Any())
                {
                    return uri;
                }
                else
                {
                    var path = uri.OriginalString.TrimEnd('/');
                    var query = string.Join("&", queryParameters);
                    return new Uri(path + "?" + query);
                }
            }
        }
  }
}