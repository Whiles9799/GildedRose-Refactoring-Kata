using System.Collections.Generic;
using System.Security.Policy;
using NUnit.Framework;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        private List<string> LegendaryItems;

        public GildedRose(IList<Item> items)
        {
            Items = items;
            LegendaryItems = new List<string>
            {
                "Sulfuras, Hand of Ragnaros"
            };
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (LegendaryItems.Contains(item.Name))
                {
                    VerifyLegendaryItem(item);
                    //"Sulfuras", being a legendary item, never has to be sold or decreases in Quality
                    break;
                }

                var qualityChange = CalculateQualityChange(item);
                item.SellIn--;
                item.Quality += qualityChange;
                item.Quality = QualityOverflowHandler(item);
            }
        }

        private int CalculateQualityChange(Item item)
        {
            if (item.Name.Contains("Backstage passes"))
            {
                return CalculateBackstagePassQualityChange(item);
            }

            var qualityChange = -1;
            //Conjured" items degrade in Quality twice as fast as normal items
            if (item.Name.Contains("Conjured"))
            {
                qualityChange *= 2;
            }

            //Once the sell by date has passed, Quality degrades twice as fast
            if (item.SellIn <= 0)
            {
                qualityChange *= 2;
            }

            //"Aged Brie" actually increases in Quality the older it gets
            if (item.Name == "Aged Brie")
            {
                qualityChange *= -1;
            }

            return qualityChange;
        }

        private int CalculateBackstagePassQualityChange(Item item)
        {
            //Quality drops to 0 after the concert
            if (item.SellIn <= 0)
            {
                return item.Quality * -1;
            }
            
            //Quality increases by 3 when there are 5 days o less 
            if (item.SellIn <= 5 && item.Quality < 50)
            {
                return 3;
            }

            //Quality increases by 2 when there are 10 days or less
            if (item.SellIn <= 10 && item.Quality < 50)
            {
                return 2;
            }

            // increases in Quality as its SellIn value approaches;
            if (item.Quality < 50)
            {
                return 1;
            }

            return 0;
        }

        private void VerifyLegendaryItem(Item item)
        {
            //While an item can never have its Quality increase above 50, "Sulfuras" is a legendary item and has a fixed Quality of 80.
            if (item.SellIn != 0)
            {
                item.SellIn = 0;
            }
        }

        private int QualityOverflowHandler(Item item)
        {
            //The Quality of an item is never negative
            if (item.Quality < 0)
            {
                return 0;
            }
            
            //The Quality of an item is never more than 50
            if (item.Quality > 50)
            {
                return 50;
            }

            return item.Quality;
        }
    }
}