using Sitecore.Data.Comparers;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.Support.Data.Comparers
{
  public class ReverseComparer : ExtractedKeysComparer
  {
    public override IKey ExtractKey(Item item)
    {
      Assert.ArgumentNotNull(item, "item");
      return new KeyObj
      {
        Item = item,
        Key = item.Name,
        Sortorder = item.Appearance.Sortorder
      };
    }

    protected override int CompareKeys(IKey key1, IKey key2)
    {
      Assert.ArgumentNotNull(key1, "key1");
      Assert.ArgumentNotNull(key2, "key2");
      return CompareNames(key1.Key.ToString(), key2.Key.ToString());
    }

    protected override int DoCompare(Item item1, Item item2)
    {
      string name = item1.Name;
      string str2 = item2.Name;
      return CompareNames(name, str2);
    }

    private static int CompareNames(string name1, string name2)
    {
      Assert.ArgumentNotNull(name1, "name1");
      Assert.ArgumentNotNull(name2, "name2");
      if ((name1.Length > 0) && (name2.Length > 0))
      {
        if ((name1[0] == '_') && (name2[0] != '_'))
        {
          return -1;
        }
        if ((name2[0] == '_') && (name1[0] != '_'))
        {
          return 1;
        }
      }
      return name2.CompareTo(name1);
    }
  }
}