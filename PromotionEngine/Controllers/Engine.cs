using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
  public class Engine
  {
    private IEnumerable<Item_Price> PriceList;
    private IEnumerable<Promotion> Promotions;

    public Engine(IEnumerable<Item_Price> priceList, IEnumerable<Promotion> promotions)
    {
      PriceList = priceList;
      Promotions = promotions;
    }

    public void CheckOut(Order order)
    {
      var foundItems = new List<Item>();
      if (Promotions != null && Promotions.Count() > 0)
        foreach (var promotion in Promotions)
        {
          var validatedItems = promotion.Validate(order, foundItems);
          UpdateValidatedItems(foundItems, validatedItems);
        }

      ApplyRegularPrice(order, foundItems);
    }

    private void ApplyRegularPrice(Order order, List<Item> foundItems)
    {
      foreach (var item in order.Items)
      {
        var validateItem = foundItems.FirstOrDefault(x => x.ItemId == item.ItemId) ?? item;
        var quantity = validateItem.Qty;
        if (quantity > 0)
          order.GrandTotal += quantity * PriceList.First(x => x.SKU_Id == item.ItemId).UnitPrice;
      }
    }

    private static void UpdateValidatedItems(List<Item> foundItems, IEnumerable<Item> validatedItems)
    {
      if (validatedItems == null || validatedItems.Count() < 1)
        return;

      foreach (var item in validatedItems)
        if (!foundItems.Any(x => x.ItemId == item.ItemId))
          foundItems.Add(item);
    }
  }
}
