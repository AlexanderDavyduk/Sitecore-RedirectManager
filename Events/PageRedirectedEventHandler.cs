namespace Sitecore.SharedSource.RedirectManager.Events
{
  using System;
  using Sitecore.SharedSource.RedirectManager.Utils;

  public class PageRedirectedEventHandler
  {
    public virtual void OnPageRedirected(object sender, EventArgs e)
    {
      var eventArgumets = e as PageRedirectedEventArgs;
      if (eventArgumets != null)
      {
        RedirectProcessor.UpdateLastUseInThread(eventArgumets);
        LogManager.WriteInfo($"Update last use for {eventArgumets.RedirectId} is completed");
      }
    }
  }
}