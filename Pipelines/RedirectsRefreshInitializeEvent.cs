namespace Sitecore.SharedSource.RedirectManager.Pipelines
{
  using Sitecore.Foundation.RedirectManager.Events;
  using System;
  using Sitecore.Events;
  using Sitecore.Pipelines;

  public class RedirectsRefreshInitializeEvent
  {
    public void Process(PipelineArgs args)
    {
      var action = new Action<RedirectsRefreshEvent>(this.RaiseRemoteEvent);
      Eventing.EventManager.Subscribe(action);
    }

    private void RaiseRemoteEvent(RedirectsRefreshEvent @event)
    {
      Event.RaiseEvent("redirects:refresh:remote", new object[] {  });
    }
  }
} 