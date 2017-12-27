using System;
using Sitecore.Events;
using Sitecore.Pipelines;
using Sitecore.SharedSource.RedirectManager.Events;

namespace Sitecore.SharedSource.RedirectManager.Pipelines
{
  public class PageRedirectedInitializeEvent
  {
    public void Process(PipelineArgs args)
    {
      var action = new Action<PageRedirectedEvent>(this.RaiseRemoteEvent);
      Eventing.EventManager.Subscribe(action);
    }

    private void RaiseRemoteEvent(PageRedirectedEvent @event)
    {
      PageRedirectedEventArgs args = new PageRedirectedEventArgs(@event);
      Event.RaiseEvent("page:redirected:remote", new object[] { args });
    }
  }
} 