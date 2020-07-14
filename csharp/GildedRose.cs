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
            if (item.Name.Contains("Conjured"))
            {
                qualityChange *= 2;
            }

            if (item.SellIn <= 0)
            {
                qualityChange *= 2;
            }

            if (item.Name == "Aged Brie")
            {
                qualityChange *= -1;
            }

            return qualityChange;
        }

        private int CalculateBackstagePassQualityChange(Item item)
        {
            if (item.SellIn <= 0)
            {
                return item.Quality * -1;
            }

            if (item.SellIn <= 5 && item.Quality < 50)
            {
                return 3;
            }

            if (item.SellIn <= 10 && item.Quality < 50)
            {
                return 2;
            }

            if (item.Quality < 50)
            {
                return 1;
            }

            return 0;
        }

        private void VerifyLegendaryItem(Item item)
        {
            if (item.SellIn != 0)
            {
                item.SellIn = 0;
            }
        }

        private int QualityOverflowHandler(Item item)
        {
            if (item.Quality < 0)
            {
                return 0;
            }

            if (item.Quality > 50)
            {
                return 50;
            }

            return item.Quality;
        }
    }
}