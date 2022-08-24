using System;
using System.Linq;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using RSSFeedReader.RobotCodes.Modules;

namespace RSSFeedReaderBot.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {

           new RSSFeedReader.Robot.Bot.BotController("5192249037:AAE5Qoqwjrp0YqrJw0LIneMR-ZSgMAVMs8A").StartBot();

           Console.ReadKey();
        }
    }
}
