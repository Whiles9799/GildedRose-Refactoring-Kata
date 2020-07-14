using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void Item_InStoreForOneDay_SellInDecreasesBy1()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Tomato", SellIn = 5, Quality = 0 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(4, items[0].SellIn);
        }
        
        [Test]
        public void Item_InStoreForOneDay_QualityDecreasesBy1()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Tomato", SellIn = 5, Quality = 5 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(4, items[0].Quality);
        }

        [Test]
        public void ItemPastSellByDate_InStoreForOneDay_QualityDecreaseBy2()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Tomato", SellIn = -1, Quality = 5 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(3, items[0].Quality);
        }

        [Test]
        public void ItemWith0Quality_InStoreForOneDay_QualityRemains0()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Tomato", SellIn = 5, Quality = 0 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(0, items[0].Quality);
        }
        
        [Test]
        public void ItemWith0or1QualityPastSellByDate_InStoreForOneDay_QualityRemains0()
        {
            IList<Item> items = new List<Item>();
            foreach (var quality in Enumerable.Range(0, 2))
            {
                items.Add(new Item{Name = "Tomato", SellIn = 10, Quality = quality});
            }
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.AreEqual(0, item.Quality);
            }
        }
        
        [Test]
        public void AgedBrie_InStoreForOneDay_QualityIncreasesBy1()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 5 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(6, items[0].Quality);
            Assert.AreEqual(4, items[0].SellIn);
        }
        
        [Test]
        public void AgedBriePastSellByDate_InStoreForOneDay_QualityIncreasesBy2()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = -1, Quality = 5 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(7, items[0].Quality);
            Assert.AreEqual(-2, items[0].SellIn);
        }
        
        [Test]
        public void AgedBrieOf50Quality_InStoreForOneDay_QualityRemains50()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(50, items[0].Quality);
            Assert.AreEqual(4, items[0].SellIn);
        }
        
        [Test]
        public void AgedBriePastSellByDateOf49or50Quality_InStoreForOneDay_QualityRemains50()
        {
            IList<Item> items = new List<Item>();
            foreach (var quality in Enumerable.Range(49, 2))
            {
                items.Add(new Item{Name = "Aged Brie", SellIn = -5, Quality = quality});
            }
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.AreEqual(50, item.Quality);
            }
        }

        [Test]
        public void Sulfuras_InStoreForOneDay_MaintainsQuality()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(80, items[0].Quality);
        }
        
        [Test]
        public void SulfurasWithNon0SellIn_InStoreForOneDay_SellInSetTo0()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(0, items[0].SellIn);
        }
        
        [Test]
        public void SulfurasPastSellByDate_InStoreForOneDay_MaintainsQuality()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -5, Quality = 80 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(80, items[0].Quality);
        }
        
        [Test]
        public void BackstagePassWith11to20SellIn_InStoreForOneDay_QualityIncreasesBy1()
        {
            IList<Item> items = new List<Item>();
            foreach (var sellIn in Enumerable.Range(11, 10))
            {
                items.Add(new Item{Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 40});
            }
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.AreEqual(41, item.Quality);
            }
        }
        
        [Test]
        public void BackstagePassWith20SellInAnd50Quality_InStoreForOneDay_MaintainsQuality()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 50 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(50, items[0].Quality);
        }
        
        [Test]
        public void BackstagePassWith6to10SellIn_InStoreForOneDay_QualityIncreasesBy2()
        {
            IList<Item> items = new List<Item>();
            foreach (var sellIn in Enumerable.Range(6, 5))
            {
                items.Add(new Item{Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 40});
            }
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.AreEqual(42, item.Quality);
            }
        }
        
        [Test]
        public void BackstagePassWith10SellInAnd49to50Quality_InStoreForOneDay_MaintainsQuality()
        {
            IList<Item> items = new List<Item>();
            foreach (var quality in Enumerable.Range(49, 2))
            {
                items.Add(new Item{Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = quality});
            }
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.AreEqual(50, item.Quality);
            }
        }
        
        [Test]
        public void BackstagePassWith1To5SellIn_InStoreForOneDay_QualityIncreasesBy3()
        {
            IList<Item> items = new List<Item>();
            foreach (var sellIn in Enumerable.Range(1, 5))
            {
                items.Add(new Item{Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 40});
            }
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.AreEqual(43, item.Quality);
            }
        }
        
        [Test]
        public void BackstagePassWith5SellInAnd50Quality_InStoreForOneDay_MaintainsQuality()
        {
            IList<Item> items = new List<Item>();
            foreach (var quality in Enumerable.Range(48, 3))
            {
                items.Add(new Item{Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = quality});
            }
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.AreEqual(50, item.Quality);
            }
        }
        
        [Test]
        public void BackstagePassWith0SellIn_InStoreForOneDay_QualityDropsTo0()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 40 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(0, items[0].Quality);
        }
        
        [Test]
        public void BackstagePassWith0SellInAnd50Quality_InStoreForOneDay_QualityDropsTo0()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 50 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(0, items[0].Quality);
        }
        
        [Test]
        public void ConjuredItem_InStoreForOneDay_SellInDecreasesBy1()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Conjured Tomato", SellIn = 5, Quality = 0 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(4, items[0].SellIn);
        }
        
        [Test]
        public void ConjuredItem_InStoreForOneDay_QualityDecreasesBy2()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Conjured Tomato", SellIn = 5, Quality = 5 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(3, items[0].Quality);
        }

        [Test]
        public void ConjuredItemPastSellByDate_InStoreForOneDay_QualityDecreaseBy4()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Conjured Tomato", SellIn = -1, Quality = 5 } };
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();
            
            Assert.AreEqual(1, items[0].Quality);
        }

        [Test]
        public void ConjuredItemWith0to2Quality_InStoreForOneDay_QualityIs0()
        {
            IList<Item> items = new List<Item>();
            foreach (var quality in Enumerable.Range(0, 3))
            {
                items.Add(new Item{Name = "Conjured Tomato", SellIn = 10, Quality = quality});
            }
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.AreEqual(0, item.Quality);
            }
        }
        
        [Test]
        public void ConjuredItemWith0to4QualityPastSellByDate_InStoreForOneDay_QualityIs0()
        {
            IList<Item> items = new List<Item>();
            foreach (var quality in Enumerable.Range(0, 5))
            {
                items.Add(new Item{Name = "Conjured Tomato", SellIn = -1, Quality = quality});
            }
            GildedRose app = new GildedRose(items);
            
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.AreEqual(0, item.Quality);
            }
        }
        
        
    }
}
