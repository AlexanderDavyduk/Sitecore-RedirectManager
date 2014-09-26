// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedirectItem.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Defines the RedirectItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager.Templates
{
  using Sitecore.Data;

  /// <summary>
  /// RedirectItem class
  /// </summary>
  public class RedirectItem
  {
    /// <summary>
    /// Gets or sets the item ID.
    /// </summary>
    /// <value>
    /// The item ID.
    /// </value>
    public ID ItemId { get; set; }

    /// <summary>
    /// Gets or sets the base url.
    /// </summary>
    /// <value>
    /// The base url.
    /// </value>
    public string Base { get; set; }

    /// <summary>
    /// Gets or sets the target url.
    /// </summary>
    /// <value>
    /// The target url.
    /// </value>
    public string Target { get; set; }

    /// <summary>
    /// Gets or sets the multisites.
    /// </summary>
    /// <value>
    /// The multisites.
    /// </value>
    public string Multisites { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [use on default].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [use on default]; otherwise, <c>false</c>.
    /// </value>
    public bool UseOnDefault { get; set; }

    /// <summary>
    /// Gets or sets the target query string.
    /// </summary>
    /// <value>
    /// The target query string.
    /// </value>
    public string TargetQueryString { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="RedirectItem"/> is external.
    /// </summary>
    /// <value>
    ///   <c>true</c> if external; otherwise, <c>false</c>.
    /// </value>
    public bool External { get; set; }

    /// <summary>
    /// Gets or sets the redirect code.
    /// </summary>
    /// <value>
    /// The redirect code.
    /// </value>
    public int RedirectCode { get; set; }
  }
}