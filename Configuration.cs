// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Configuration.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Defines the Configuration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager
{
  using Sitecore.Configuration;

  /// <summary>
  ///  Configuration class
  /// </summary>
  public class Configuration
  {
    /// <summary>
    /// Gets a value indicating whether module should write the logs.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the module should write logs; otherwise, <c>false</c>.
    /// </value>
    public static bool EnableLogging
    {
      get
      {
        return Settings.GetBoolSetting("Sitecore.SharedSource.RedirectManager.EnableLogging", false);
      }
    }

    /// <summary>
    /// Gets a value indicating whether [enable not founds].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [enable not founds]; otherwise, <c>false</c>.
    /// </value>
    public static bool EnableNotFounds
    {
      get
      {
        return Settings.GetBoolSetting("Sitecore.SharedSource.RedirectManager.EnableNotFounds", false);
      }
    }

    /// <summary>
    /// Gets a value indicating whether [enable multisites].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [enable multisites]; otherwise, <c>false</c>.
    /// </value>
    public static bool EnableMultisites
    {
      get
      {
        return Settings.GetBoolSetting("Sitecore.SharedSource.RedirectManager.EnableMultisites", false);
      }
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Configuration"/> is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if module is enabled; otherwise, <c>false</c>.
    /// </value>
    public static bool Enabled
    {
      get
      {
        return Settings.GetBoolSetting("Sitecore.SharedSource.RedirectManager.Enabled", false);
      }
    }

    /// <summary>
    /// Gets the context database.
    /// </summary>
    public static string Database
    {
      get
      {
        return Settings.GetSetting("Sitecore.SharedSource.RedirectManager.Database", "master");
      }
    }

    /// <summary>
    /// Gets the redirect status code.
    /// </summary>
    public static int RedirectStatusCode
    {
      get
      {
        return Settings.GetIntSetting("Sitecore.SharedSource.RedirectManager.RedirectStatusCode", 301);
      }
    }

    /// <summary>
    /// Gets a value indicating whether module should check the duplicates.
    /// </summary>
    /// <value>
    ///   <c>true</c> if module should check the duplicates; otherwise, <c>false</c>.
    /// </value>
    public static bool CheckDuplicates
    {
      get
      {
          return Settings.GetBoolSetting("Sitecore.SharedSource.RedirectManager.CheckDuplicates", false);
      }
    }

    /// <summary>
    /// Gets the URL validation Regex.
    /// </summary>
    public static string UrlValidation
    {
      get
      {
        return Settings.GetSetting("Sitecore.SharedSource.RedirectManager.UrlValidation", string.Empty);
      }
    }

    /// <summary>
    /// Gets the validation error message.
    /// </summary>
    public static string ValidationErrorMessage
    {
        get
        {
            return Settings.GetSetting("Sitecore.SharedSource.RedirectManager.ValidationErrorMessage", string.Empty);
        }
    }

    /// <summary>
    /// Gets the ignore pages list.
    /// </summary>
    public static string[] IgnorePages
    {
      get
      {
        return Settings.GetSetting("Sitecore.SharedSource.RedirectManager.IgnorePages", string.Empty).Split('|');
      }
    }

    /// <summary>
    /// Gets a value indicating whether we need to check presentation for target items and create redirects only for items with presentation.
    /// </summary>
    /// <value>
    ///   <c>true</c> if need; otherwise, <c>false</c>.
    /// </value>
    public static bool CheckPresentation
    {
      get
      {
        return Settings.GetBoolSetting("Sitecore.SharedSource.RedirectManager.CheckPresentation", false);
      }
    }

    /// <summary>
    /// Gets a value indicating whether enable cycling protection.
    /// </summary>
    /// <value>
    /// <c>true</c> if cycling protection is enabled; otherwise, <c>false</c>.
    /// </value>
    public static bool EnableCyclingProtection
    {
      get
      {
        return Settings.GetBoolSetting("Sitecore.SharedSource.RedirectManager.EnableCyclingProtection", false);
      }
    }

    /// <summary>
    /// Gets the admissible number of cycles.
    /// </summary>
    /// <value>
    /// The admissible number of cycles.
    /// </value>
    public static int RecursiveAttempts
    {
      get
      {
        return Settings.GetIntSetting("Sitecore.SharedSource.RedirectManager.RecursiveAttempts", 3);
      }
    }

    /// <summary>
    /// Gets the removal date.
    /// </summary>
    /// <value>
    /// The removal date.
    /// </value>
    public static int RemovalDate
    {
      get
      {
        return Settings.GetIntSetting("Sitecore.SharedSource.RedirectManager.RemovalDate", 60);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether redirects list is created.
    /// </summary>
    /// <value>
    /// <c>true</c> if created; otherwise, <c>false</c>.
    /// </value>
    public static bool RedirectsListIsInitialized { get; set; }
  }
}