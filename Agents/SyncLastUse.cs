namespace Sitecore.SharedSource.RedirectManager.Agents
{
  using System;
  using System.CodeDom;
  using System.Diagnostics;
  using Sitecore.Configuration;
  using Data.Events;
  using SecurityModel;
  using Utils;
  using System.Linq;
  using System.Threading;

  public class SyncLastUse
  {
    public void Sync()
    {
      if (!Configuration.UpdateLastUse || !Configuration.WriteLastUseToMongo)
      {
        return;
      }

      LogManager.WriteInfo("SyncLastUse job has been started");
      var sw = new Stopwatch();
      sw.Start();
      try
      {
        using (new SecurityDisabler())
        {
          using (new EventDisabler())
          {
            var repository = new Repository.RedirectsRepository();
            var redirectsFromMongo = repository.GetRedirects();
            if (!redirectsFromMongo.Any())
            {
              sw.Stop();
              LogManager.WriteInfo("SyncLastUse job - there are no redirects in Mongo");
              return;
            }

            foreach (var redirect in redirectsFromMongo)
            {
              var item = Factory.GetDatabase(Configuration.LastUseDatabaseName).GetItem(redirect.RedirectId.ToString());
              if (item == null || !item.IsItemOfType(Templates.Settings.TemplateId))
              {
                repository.RemoveRedirect(redirect.RedirectId);
                continue;
              }

              var settingItem = new Templates.Settings(item);
              if (settingItem.LastUse.DateTime.Date < redirect.LastUse.Date)
              {
                settingItem.UpdateLastUse(System.DateTime.Now);
                LogManager.WriteInfo($"The LastUse field was updated with value \"{redirect.LastUse.Date:dd/MM/yyyy}\" for \"{settingItem.Name}\" item with id {settingItem.ID}");
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        LogManager.WriteError($"{ex.Message} {ex.StackTrace}");
      }
      finally
      {
        sw.Stop();
        LogManager.WriteInfo($"SyncLastUse job is finished, elapsed time  - {sw.Elapsed}");
      }
    }

    public void SyncInThread()
    {
      var newThread = new Thread(this.Sync);
      newThread.Start();
    }
  }
}