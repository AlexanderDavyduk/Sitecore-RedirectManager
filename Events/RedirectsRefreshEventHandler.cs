namespace Sitecore.SharedSource.RedirectManager.Events
{
  using System;
  using Sitecore.SharedSource.RedirectManager.Utils;

  public class RedirectsRefreshEventHandler
  {
    public virtual void RefreshRedirects(object sender, EventArgs e)
    {
      RedirectProcessor.CreateListOfRedirectsInThread();
      LogManager.WriteInfo($"Refresh redirects is completed");
    }
  }
}