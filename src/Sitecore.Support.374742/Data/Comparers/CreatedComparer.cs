using Sitecore.Data.Comparers;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;

namespace Sitecore.Support.Data.Comparers
{
  public class CreatedComparer : Sitecore.Data.Comparers.CreatedComparer
  {
    public override IKey ExtractKey(Item item)
    {
      Assert.ArgumentNotNull(item, "item");
      //Spesifying of the Sortorder value fixes the issue
      return new KeyObj
      {
        Item = item,
        Key = this.GetCreationDate(item),
        Sortorder = item.Appearance.Sortorder
      };
    }

    private DateTime GetCreationDate(Item item)
    {
      DateTime maxValue = DateTime.MaxValue;
      foreach (Item item2 in item.Versions.GetVersions(true))
      {
        DateTime created = item2.Statistics.Created;
        if ((created != DateTime.MinValue) && (created.CompareTo(maxValue) < 0))
        {
          maxValue = created;
        }
      }
      if (!(maxValue == DateTime.MaxValue))
      {
        return maxValue;
      }
      return DateTime.MinValue;
    }
  }
}