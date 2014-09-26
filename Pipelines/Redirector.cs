// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Redirector.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Defines the Redirector type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager.Pipelines
{
  using System;
  using System.Diagnostics;
  using System.Linq;
  using Sitecore.Diagnostics;
  using Sitecore.Pipelines.HttpRequest;
  using Sitecore.SharedSource.RedirectManager.Utils;

  /// <summary>
  ///  Redirector class
  /// </summary>
  public class Redirector : HttpRequestProcessor
  {
    /// <summary>
    /// Checks the acceptable page mode and args and redirect page if needed
    /// </summary>
    /// <param name="args">The args.</param>
    public override void Process(HttpRequestArgs args)
    {
      Assert.ArgumentNotNull(args, "RedirectManager");

      if (!Configuration.Enabled)
      {
        return;
      }

      try
      {
        if (!Configuration.RedirectsListIsInitialized)
        {
          Configuration.RedirectsListIsInitialized = true;
          RedirectProcessor.Initialize();
          RedirectProcessor.CreateListOfRedirectsInThread();
        }
      }
      catch (Exception e)
      {
        LogManager.WriteError(e.Message);
        LogManager.WriteError(e.StackTrace);
      }

      if (Context.Item != null)
      {
        CyclingProtectionManager.ClearCurrentCycle(args.Context.Response, args.Context.Request);
        if (RedirectProcessor.CheckPresentation(Context.Item))
        {
          return;
        }
      }

      if (!CheckPageMode() || Context.Database == null || Context.Request.FilePath == null
        || Context.Database.Name == "core" || CheckIgnorePages())
      {
        CyclingProtectionManager.ClearCurrentCycle(args.Context.Response, args.Context.Request);
        return;
      }

      if (!CyclingProtectionManager.CheckCycle(args.Context.Response, args.Context.Request))
      {
        LogManager.WriteInfo(string.Format("Reached limit of cycles for the request: \"{0}\"", Context.Request.FilePath));
        CyclingProtectionManager.ClearCurrentCycle(args.Context.Response, args.Context.Request);
        return;
      }

      var sw = new Stopwatch();
      sw.Start();

      int redirectCode;
      string redirectId;
      var baseUrl = UrlNormalizer.CheckPageExtension(UrlNormalizer.Normalize(Context.Request.FilePath, true));
      var targetUrl = RedirectProcessor.FindRedirect(baseUrl, out redirectCode, out redirectId);
      if (string.IsNullOrEmpty(targetUrl))
      {
        if (Configuration.EnableNotFounds)
        {
          LogManager.WriteInfo(string.Format("Redirect for the page: \"{0}\" was not found", Context.Request.FilePath));
        }

        CyclingProtectionManager.ClearCurrentCycle(args.Context.Response, args.Context.Request);
        return;
      }

      RedirectProcessor.UpdateLastUseInThread(redirectId);
      sw.Stop();
      LogManager.WriteInfo(
        string.Format(
          "Page \"{0}\" was redirected to \"{1}\": redirect item id - {2}, elapsed time - {3} milliseconds",
          Context.Request.FilePath,
          targetUrl,
          redirectId,
          sw.ElapsedMilliseconds));

      Response(args, targetUrl, redirectCode);
    }

    /// <summary>
    /// Redirect requested page.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <param name="responseUrl">The response URL.</param>
    /// <param name="redirectCode">The redirect code.</param>
    private static void Response(HttpRequestArgs args, string responseUrl, int redirectCode)
    {
      args.Context.Response.Clear();
      args.Context.Response.StatusCode = redirectCode;
      args.Context.Response.RedirectLocation = responseUrl;
      args.Context.Response.End();
    }

    /// <summary>
    /// Checks the acceptable page mode.
    /// </summary>
    /// <returns>Acceptable page mode - true, otherwise - false</returns>
    private static bool CheckPageMode()
    {
      return Context.PageMode.IsNormal && !Context.PageMode.IsPageEditor && !Context.PageMode.IsPageEditorEditing
             && !Context.PageMode.IsPreview;
    }

    /// <summary>
    /// Checks the ignore pages.
    /// </summary>
    /// <returns>Is ignore page</returns>
    private static bool CheckIgnorePages()
    {
      return Configuration.IgnorePages.Length > 0 && Configuration.IgnorePages.Any(prefix => Context.Request.FilePath.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }
  }
}