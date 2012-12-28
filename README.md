GitHub Issues via Feedback Form
===============================

Push user feedback from your website directly to [GitHub Issues](https://github.com/blog/831-issues-2-0-the-next-generation). This CSharp ASP.NET MVC 3 project uses [RestSharp](https://github.com/restsharp/RestSharp) to connect with the [GitHub API v3](http://developer.github.com/v3/). [See an example of an issue created from the website](https://github.com/otint/github-issues-via-feedback-form/issues/7).

## Building

To run the project you'll have to first rename the file `appSettings.dll.config.example` to `appSettings.dll.config`. Open the renamed file and substitute the values in this section with your information:

```csharp
<appSettings>
  <add key="username" value="your github username"/>
  <add key="password" value="your github password"/>
  <add key="resource" value="/repos/github-username/github-repo/issues"/>
  <add key="defaultlabel" value="feedback"/>
</appSettings>
```

With the `defaultlabel` you can enter anything you want, if the label is not already created for this repo it will be created on the fly. We chose to use a unique label named `feedback` to distinguish issues generated directly by website users. You could also assign more than one label to the issue by separating each label with a comma.


## Side note

You'll see that the issue created contains some useful troubleshooting information about the user's browser. It would be useful if the issue also contained a screenshot of the user's browser at the time they submitted the feedback. Anyone know how to accomplish that?
 
## License

Licensed under the Apache License, Version 2.0, included in the repo.
