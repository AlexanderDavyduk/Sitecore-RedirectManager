// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegExRedirect.cs">
//   Copyright (C) 2012 by Alexander Davyduk. All rights reserved.
// </copyright>
// <summary>
//   Defines the RegExRedirect type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.SharedSource.RedirectManager.Templates
{
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;

  /// <summary>
  /// Defines the regex redirect class.
  /// </summary>
  public class RegExRedirect : CustomItem
  {
    // Fields

    /// <summary>
    ///  SectionToItemTemplate template ID
    /// </summary>
    public const string TemplateId = "{F91102D6-C17E-4966-9A1A-F551FDD18D40}";

    /// <summary>
    /// Base Section url field
    /// </summary>
    private TextField baseSection;

    /// <summary>
    /// Target Item field
    /// </summary>
    private LinkField targetItem;

    /// <summary>
    /// The redirect code
    /// </summary>
    private int redirectCode;

    /// <summary>
    /// The date of last use
    /// </summary>
    private DateField lastUse;

    // Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="RegExRedirect"/> class. 
    /// Initializes a new instance of the <see cref="SectionToItem"/> class.
    /// </summary>
    /// <param name="innerItem">
    /// Inner item.
    /// </param>
    public RegExRedirect(Item innerItem)
      : base(innerItem)
    {
    }

    // Properties

    /// <summary>
    /// Gets the regular expression url.
    /// </summary>
    public TextField Expression
    {
      get
      {
        return this.baseSection ?? (this.baseSection = this.InnerItem.Fields["Expression"]);
      }
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public LinkField Value
    {
      get
      {
        return this.targetItem ?? (this.targetItem = this.InnerItem.Fields["Value"]);
      }
    }

    /// <summary>
    /// Gets the redirect code.
    /// </summary>
    /// <value>
    /// The redirect code.
    /// </value>
    public int RedirectCode
    {
      get
      {
        return this.redirectCode == 0 ? (this.redirectCode = MainUtil.GetInt(this.InnerItem.Fields["Redirect Code"].Value, Configuration.RedirectStatusCode)) : this.redirectCode;
      }
    }

    /// <summary>
    /// Gets the last use.
    /// </summary>
    /// <value>
    /// The last use.
    /// </value>
    public DateField LastUse
    {
      get
      {
        return this.lastUse ?? (this.lastUse = this.InnerItem.Fields["Last Use"]);
      }
    }
  }
}