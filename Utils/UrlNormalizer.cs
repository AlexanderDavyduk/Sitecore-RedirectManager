// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NormalizeUrl.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Defines the NormalizeUrl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager.Utils
{
  using Sitecore.Data.Items;
  using Sitecore.Links;
  using Sitecore.Sites;

  /// <summary>
  ///  NormalizeUrl class
  /// </summary>
  public class UrlNormalizer
  {
    /// <summary>
    ///  Start item for the website
    /// </summary>
    private static string startItem;

    /// <summary>
    ///  Virtual folder for the website
    /// </summary>
    private static string virtualFolder;

    /// <summary>
    ///  Url options
    /// </summary>
    private static UrlOptions _urlOptions;

    private static UrlOptions UrlOptions
    {
      get
      {
        if (_urlOptions == null)
        {
          _urlOptions = new UrlOptions { LanguageEmbedding = LanguageEmbedding.Never, AddAspxExtension = true };
        }

        return _urlOptions;
      }
    }

  

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public static void Initialize()
    {
      using (new SiteContextSwitcher(SiteContext.GetSite(Configuration.DefaultSiteName)))
      {
        startItem = Context.Site.StartItem.ToLower();
        virtualFolder = GetVirtualVolder();
      }
    }

    /// <summary>
    /// Gets the virtual volder for the context site.
    /// </summary>
    /// <returns>Virtual folder for the context site</returns>
    public static string GetVirtualVolder()
    {
      if (Context.Site != null)
      {
        var folder = DecodeUrl(Context.Site.VirtualFolder.ToLower());
        return folder != "/" ? folder.TrimEnd('/') : string.Empty;
      }

      return string.Empty;
    }

    /// <summary>
    /// Normalizes the specified URL.
    /// </summary>
    /// <param name="url">
    /// The URL.
    /// </param>
    /// <param name="currentSite">
    /// Should this method use the current site or not for the strat item and virtual folder.
    /// </param>
    /// <returns>
    /// Normalized url
    /// </returns>
    public static string Normalize(string url, bool currentSite = false)
    {
      if (!string.IsNullOrEmpty(url))
      { 
        string currentStartItem, currentVirtualFolder;
        if (currentSite)
        {
          currentStartItem = Context.Site.StartItem.ToLower();
          currentVirtualFolder = GetVirtualVolder();
        }
        else
        {
          currentStartItem = startItem;
          currentVirtualFolder = virtualFolder;
        }

        if (url != "/")
        {
          url = DecodeUrl(url.ToLower()).TrimEnd('/');
        }
        
        if (!url.StartsWith("/"))
        {
          url = string.Format("/{0}", url);
        }

        url = RemoveStartPathFromUrl(url, currentVirtualFolder);
        url = RemoveStartPathFromUrl(url, currentStartItem); 
        
        return url;
      }

      return string.Empty;
    }


    /// <summary>
    /// Get the query string.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="queryString">The query string.</param>
    /// <returns>
    /// Url without query string.
    /// </returns>
    public static string GetQueryStringFromUrl(string url, out string queryString)
    {
      queryString = string.Empty;
      if (url.Contains("?"))
      {
        var urlContainer = url.Split('?');
        if (urlContainer.Length > 1)
        {
          url = urlContainer[0];
          queryString = urlContainer[1];
        }
      }

      return url;
    }

    /// <summary>
    /// Checks the page extension.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>Checked url</returns>
    public static string CheckPageExtension(string url)
    {
      if (!string.IsNullOrEmpty(url))
      {
        if (url.EndsWith(".aspx") && !Configuration.IncludePageExtention)
        {
          url = RemovePageExtension(url);
        }

        if (!url.EndsWith(".aspx") && Configuration.IncludePageExtention)
        {
          url = string.Format("{0}.aspx", url);
        }
      }

      return url;
    }

    /// <summary>
    /// Removes the page extension.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>
    /// The url without page extension.
    /// </returns>
    public static string RemovePageExtension(string url)
    {
      const string Aspx = ".aspx";

      return url.EndsWith(Aspx) ? url.Substring(0, url.LastIndexOf(Aspx, System.StringComparison.Ordinal)) : url;
    }

    /// <summary>
    /// Gets the item URL.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>item url</returns>
    public static string GetItemUrl(Item item)
    {
      if (item == null)
      {
        return string.Empty;
      }

      using (new SiteContextSwitcher(SiteContext.GetSite(Configuration.DefaultSiteName)))
      {
        return LinkManager.GetItemUrl(item, UrlOptions).ToLower();
      }
    }

    /// <summary>
    /// Encodes the URL.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>Url with replaced spases</returns>
    public static string EncodeUrl(string url)
    {
      return url.Replace(" ", "-");
    }

    /// <summary>
    /// Decodes the URL.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>Url with replated "_" and "-"</returns>
    public static string DecodeUrl(string url)
    {
      return url.Replace("-", " ");
    }

    /// <summary>
    /// Removes the start path from URL.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="startPath">The start path.</param>
    /// <returns>
    /// The URL without start path.
    /// </returns>
    private static string RemoveStartPathFromUrl(string url, string startPath)
    {
      if (!string.IsNullOrEmpty(startPath)
        && url.StartsWith(startPath)
          && url.Length > startPath.Length
            && url[startPath.Length] == '/')
      {
        url = url.Remove(0, startPath.Length);
      }

      return url;
    }
  }
}