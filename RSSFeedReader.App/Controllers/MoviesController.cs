using System.Collections.Generic;
using System.Linq;
using System.Timers;
using CodeHollow.FeedReader;
using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.DataLayer.Services;
using RSSFeedReader.Robot.Bot;
using RSSFeedReader.RobotCodes.Modules;

namespace RSSFeedReader.App.Controllers
{
    public class MoviesController
    {
        private static List<FeedItem> feedItems;

        public MoviesController()
        {
            feedItems = FeedController.GetFeedItems(
                "https://iptorrents.com/t.rss?u=1764728;tp=55ec80a8b063cabd26022bde0d0c268f;48;7;20;38;100;101;89;68;62;6;90;96;87;77;54").ToList();


            Timer timer = new Timer(300000);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.Start();


        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var newItems = FeedController.GetFeedItems(
                "https://iptorrents.com/t.rss?u=1764728;tp=55ec80a8b063cabd26022bde0d0c268f;48;7;20;38;100;101;89;68;62;6;90;96;87;77;54").ToList();
            newItems = newItems.Except(feedItems, new FeedComparer()).ToList();
            await MessagesController.SendMessage(new MainRepository().GetActiveUsers(), newItems);
        }
    }
}
