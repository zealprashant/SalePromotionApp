using System;
using System.Collections.Generic;
using PromotionEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestPromotionEngine
{
  [TestClass]
  public class UnitTest1
  {
    // test set up as per instructions
    static readonly IEnumerable<Item_Price> PriceList =
      new List<Item_Price> {
        new Item_Price { SKU_Id = 'A', UnitPrice = 50 },
        new Item_Price { SKU_Id = 'B', UnitPrice = 30 },
        new Item_Price { SKU_Id = 'C', UnitPrice = 20 },
        new Item_Price { SKU_Id = 'D', UnitPrice = 15 } };

    // test set up as per instructions
    static readonly IEnumerable<Promotion> Promotions =
      new List<Promotion> {
        new Promotion {
          Items = new List<Item> {
            new Item { ItemId = 'A', Qty = 3 }},
          GrandTotal = 130 }, // 3 of A for 130
        new Promotion {
          Items = new List<Item> {
            new Item { ItemId = 'B', Qty = 2 }},
          GrandTotal = 45 }, // 2 of B for 45
        new Promotion {
          Items = new List<Item> {
            new Item { ItemId = 'C', Qty = 1 },
            new Item { ItemId = 'D', Qty = 1 }},
          GrandTotal = 30 } }; // C + D for 30

    static readonly Engine actualEngine = new Engine(PriceList, Promotions);

    [TestMethod]
    public void Test_Scenario_A()
    {
      var order =
        new Order
        {
          Items = new List<Item>
          {
            new Item { ItemId = 'A', Qty = 1 },
            new Item { ItemId = 'B', Qty = 1 },
            new Item { ItemId = 'C', Qty = 1 }}
        };

      actualEngine.CheckOut(order);
      Assert.IsTrue(order.GrandTotal == 100);
    }

    [TestMethod]
    public void Test_Scenario_B()
    {
      var order =
        new Order
        {
          Items = new List<Item>
          {
            new Item { ItemId = 'A', Qty = 5 },
            new Item { ItemId = 'B', Qty = 5 },
            new Item { ItemId = 'C', Qty = 1 }}
        };

      actualEngine.CheckOut(order);
      Assert.IsTrue(order.GrandTotal == 370);
    }

    [TestMethod]
    public void Test_Scenario_C()
    {
      var order =
        new Order
        {
          Items = new List<Item>
          {
            new Item { ItemId = 'A', Qty = 3 },
            new Item { ItemId = 'B', Qty = 5 },
            new Item { ItemId = 'C', Qty = 1 },
            new Item { ItemId = 'D', Qty = 1 }}
        };

      actualEngine.CheckOut(order);
      Assert.IsTrue(order.GrandTotal == 280);
    }
  }
}
