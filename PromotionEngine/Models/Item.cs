using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
  public class Item
  {
    public Item() { }
    public Item(Item item)
    {
      ItemId = item.ItemId;
      Qty = item.Qty;
    }

    public char ItemId { get; set; }
    public int Qty { get; set; }

    public override string ToString()
    {
      return $"{ItemId} {Qty}";
    }
  }
}
