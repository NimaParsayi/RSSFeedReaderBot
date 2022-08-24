using CodeHollow.FeedReader;
using System.Collections.Generic;
using System.Linq;

namespace RSSFeedReader.RobotCodes.Modules
{
    public static class FeedController
    {
        public static List<FeedItem> GetFeedItems(string url)
        {
            var items = (List<FeedItem>)FeedReader.Read(url).Items;
            items = items.OrderBy(x => x.PublishingDate).ToList();
            return items;
        }
        public static string GetFeedAsString(string url)
        {
            return FeedReader.Read(url).OriginalDocument;
        }
    }
}