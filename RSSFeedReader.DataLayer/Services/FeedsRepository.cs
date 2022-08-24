using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CodeHollow.FeedReader;

namespace RSSFeedReader.DataLayer.Services
{
    public class FeedsRepository
    {
        private string FilePath { get; set; }
        public FeedsRepository()
        {
            FilePath = Path.GetFullPath("./Context/Feeds.XML");
        }

        public async Task<List<FeedItem>> GetAllFeeds()
        {
            var feed = await FeedReader.ReadFromFileAsync(FilePath);
            var feedItems = (List<FeedItem>)feed.Items;
            return await Task.FromResult(feedItems);
        }

        public async Task UpdateFeed(string text)
        {
            await File.WriteAllTextAsync(FilePath, text);
        }
    }
}
