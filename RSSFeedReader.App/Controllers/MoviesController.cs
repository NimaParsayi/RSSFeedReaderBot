using System.Collections.Generic;
using System.Linq;
using System.Timers;
using CodeHollow.FeedReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RSSFeedReader.DataLayer.Services;
using RSSFeedReader.Robot.Bot;
using RSSFeedReader.RobotCodes.Modules;

namespace RSSFeedReader.App.Controllers
{
    public class MoviesController : ControllerBase
    {
        private FeedsRepository feedsRepository;
        private IConfiguration configuration;
        public MoviesController(IConfiguration configuration)
        {
            feedsRepository = new FeedsRepository();
            this.configuration = configuration;

            Timer timer = new Timer(300000);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.Start();


        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var oldItems = await feedsRepository.GetAllFeeds();

            var newItems = FeedController.GetFeedItems(configuration.GetSection("RssLink").Value).ToList();
            newItems = newItems.Except(oldItems, new FeedComparer()).ToList();

            await feedsRepository.UpdateFeed(FeedController.GetFeedAsString(configuration.GetSection("RssLink").Value));

            await MessagesController.SendMessage(new MainRepository().GetActiveUsers(), newItems);
        }
    }
}
