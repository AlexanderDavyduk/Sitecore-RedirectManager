// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogManager.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Defines the LogManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager.Utils
{
  using log4net;

  /// <summary>
  /// LogManager class
  /// </summary>
  public class LogManager
  {
    /// <summary>
    /// The logger
    /// </summary>
    private static ILog logger;

    /// <summary>
    /// Gets the logger.
    /// </summary>
    /// <value>
    /// The logger.
    /// </value>
    public static ILog Logger
    {
      get
      {
        return logger ?? (logger = Diagnostics.LoggerFactory.GetLogger("Sitecore.Diagnostics.RedirectManager"));
      }
    }

    /// <summary>
    /// Writes the info message to the log.
    /// </summary>
    /// <param name="message">The message.</param>
    public static void WriteInfo(string message)
    {
      if (Configuration.EnableLogging)
      {
        Logger.Info(string.Format("RedirectManager: {0}", message));
      }
    }

    /// <summary>
    /// Writes the error message to the log.
    /// </summary>
    /// <param name="message">The message.</param>
    public static void WriteError(string message)
    {
      if (Configuration.EnableLogging)
      {
        Logger.Error(string.Format("RedirectManager: {0}", message));
      }
    }
  }
}