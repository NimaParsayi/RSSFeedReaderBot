
using System;
using System.Threading;
using System.Threading.Tasks;
using RSSFeedReader.DataLayer.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace RSSFeedReader.Robot.Bot
{

    public class BotController
    {
        public static ITelegramBotClient bot;
        public BotController(string botToken)
        {
            bot = new TelegramBotClient(botToken);
        }

        public void StartBot()
        {
            using var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
                ThrowPendingUpdates = true
            };
            bot.ReceiveAsync(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
        }

        private async Task HandleErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            if (arg2 is ApiRequestException apiRequestException)
            {
                await arg1.SendTextMessageAsync(123, apiRequestException.ToString());
            }
        }

        private async Task HandleUpdateAsync(ITelegramBotClient arg1, Update arg2, CancellationToken arg3)
        {
            if (arg2.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                Console.WriteLine(arg2.Message.Text);
                var mc = new MessagesController(arg1, arg2.Message, new MainRepository());
                await mc.CheckMessage();
            }
        }


        //public static async Task<bool> IsUserJoinedChannel(ITelegramBotClient bot, long userId)
        //{
        //    var user = await bot.GetChatMemberAsync(-1001731566426, userId);
        //    return user.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Member
        //        || user.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Creator
        //        || user.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Administrator
        //        ? true : false;
        //}
    }
}