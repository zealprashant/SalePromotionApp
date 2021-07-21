using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class Promotion :Order,IPromotions
    {
        public IEnumerable<Item> Validate(Order order, IEnumerable<Item> validatedItems)
        {
            var foundItems = new List<Item>();
            if (Items == null || Items.Count < 1)
                return foundItems;

            foreach (var promotionItem in Items)
            {
                var foundItem = validatedItems.FirstOrDefault(x => x.ItemId == promotionItem.ItemId) ??
                  order.Items.FirstOrDefault(x => x.ItemId == promotionItem.ItemId);
                if (foundItem == null || foundItem.Qty < promotionItem.Qty)
                    return null;

                if (!foundItems.Any(x => x.ItemId == foundItem.ItemId))
                    foundItems.Add(new Item(foundItem));
            }

            ApplyPromotionByItemsQuantity(order, foundItems);

            return foundItems;
        }

        public void ApplyPromotionByItemsQuantity(Order order, List<Item> foundItems)
        {
            var found = foundItems.Count() > 0;
            if (found)
            {
                do
                {
                    order.GrandTotal += GrandTotal;
                    foreach (var promotionItem in Items)
                    {
                        var item = foundItems.FirstOrDefault(x => x.ItemId == promotionItem.ItemId);
                        if (item != null)
                        {
                            item.Qty -= promotionItem.Qty;
                            if (found)
                                found = item.Qty >= promotionItem.Qty;
                        }
                    }
                } while (found);
            }
        }
    }
}
