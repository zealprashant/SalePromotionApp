using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
  public class Order
  {
    public List<Item> Items { get; set; }
    public double GrandTotal { get; set; }
  }
}
