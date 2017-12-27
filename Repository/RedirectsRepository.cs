namespace Sitecore.SharedSource.RedirectManager.Repository
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using MongoDB.Driver;
  using MongoDB.Driver.Builders;
  using Sitecore.SharedSource.RedirectManager.Models;

  public class RedirectsRepository : IRedirectsRepository
  {
    public const string DatabaseName = "RedirectsDB";
    public const string RedirectsCollectionName = "Redirects";
    private readonly MongoCollection<RedirectModel> redirectsСollection;

    public RedirectsRepository(string connectionStringName = "")
    {
      var connectionString = string.IsNullOrEmpty(connectionStringName) ? SharedSource.RedirectManager.Configuration.MongoConnectionStringName : connectionStringName;
      var client = new MongoClient(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString);
      var server = client.GetServer();
      var database = server.GetDatabase(DatabaseName);
      this.redirectsСollection = database.GetCollection<RedirectModel>(RedirectsCollectionName);
    }

    public void UpdateRedirect(Guid id, DateTime lastUse)
    {
      if (id == Guid.Empty || lastUse == DateTime.MinValue)
      {
        return;
      }

      var redirect = new RedirectModel
      {
        RedirectId = id,
        LastUse = lastUse,
      };

      var query = Query.EQ("RedirectId", id);
      var redirectRecord = this.redirectsСollection.FindOne(query);
      if (redirectRecord != null)
      {
        if (lastUse.Date > redirectRecord.LastUse.Date)
        {
          this.redirectsСollection.Remove(query);
          this.redirectsСollection.Insert(redirect);
        }
      }
      else
      {
        this.redirectsСollection.Insert(redirect);
      }
    }

    public List<RedirectModel> GetRedirects()
    {
      var collection = this.redirectsСollection.FindAll();
      return collection.Any() ? collection.ToList() : new List<RedirectModel>();
    }

    public void RemoveRedirect(Guid id)
    {
      if (id == Guid.Empty)
      {
        return;
      }

      var query = Query.EQ("RedirectId", id);
      var redirectRecord = this.redirectsСollection.FindOne(query);
      if (redirectRecord != null)
      {
        this.redirectsСollection.Remove(query);
      }
    }
  }
}