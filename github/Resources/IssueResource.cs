using System;
using System.Collections.Generic;
using RestSharp;

namespace github.Resources
{
  public class IssueResource : Resource
  {
    public IssueResource(IRestClient client) : base(client)
		{  
			resource = "issues";
      //creatableAttributes = new string[] { "encoding_profile_ids", "encoding_profile_tags", "skip_default_encoding_profiles", "use_original_as_transcoding" };
			accessableAttributes = new string[] {"title", "state", "labels"};
		}

    public List<Issue> List()
    { return base.List<Issue>(new Dictionary<string, object>()); }

    public List<Issue> List(Dictionary<string, object> parameters)
    { return base.List<Issue>(parameters); }

    public Issue Find(string id)
    { return base.Find<Issue>(id); }

    public Issue Create(Dictionary<string, object> parameters)
    {
      return base.Create<Issue>(parameters); }

    protected override void AddCreatableParameters(RestRequest request, Dictionary<string, object> parameters)
    {
      base.AddCreatableParameters(request, parameters);
      if (parameters.ContainsKey("addBody")) request.AddBody(parameters["addBody"]);
    }
  }
}