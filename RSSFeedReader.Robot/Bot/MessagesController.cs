using CodeHollow.FeedReader;
using RSSFeedReader.DataLayer.Services;
using RSSFeedReader.Robot.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSSFeedReader.DataLayer.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RSSFeedReader.Robot.Bot
{
    public class MessagesController
    {
        private static ITelegramBotClient bot;
        private static Message msg;
        private readonly MainRepository mainRepository;

        public MessagesController(ITelegramBotClient context, Message message, MainRepository mainRepository)
        {
            bot = context;
            msg = message;
            this.mainRepository = mainRepository;
        }

        public async Task CheckMessage()
        {
            var isExists = mainRepository.IsExists(msg.From.Id);
            if (msg.Text.StartsWith("/start"))
            {
                if (!isExists)
                {
                    var result = mainRepository.AddUser(new DataLayer.Models.User()
                    {
                        Id = msg.From.Id,
                        Status = UserStatus.Active,
                        JoinDate = DateTime.Now
                    });
                    if (result)
                        await bot.SendTextMessageAsync(msg.Chat.Id, "✅ عضویت شما با موفقیت انجام شد !" +
                                                                    "\n🔥 از این به بعد تازه ترین فیلم ها خودکار برای شما ارسال می‌شود");
                    else
                        await bot.SendTextMessageAsync(msg.Chat.Id, "❌ خطایی رخ داد !");
                }
                else
                {
                    mainRepository.ActiveUser(msg.From.Id);
                    await bot.SendTextMessageAsync(msg.Chat.Id, "❕شما در حال حاضر عضو ربات هستید.");
                }
            }
            else if (msg.Text.StartsWith("/stop"))
            {
                if (isExists)
                {
                    var result = mainRepository.InActiveUser(msg.From.Id);

                    if (result)
                        await bot.SendTextMessageAsync(msg.Chat.Id, "❌ عضویت شما با موفقیت لغو شد !");
                    else
                        await bot.SendTextMessageAsync(msg.Chat.Id, "❌ خطایی رخ داد !");

                }
                else
                    await bot.SendTextMessageAsync(msg.Chat.Id, "❕شما در حال حاضر عضو ربات نیستید.");
            }
            else if (msg.Text.StartsWith("/users"))
            {
                if (isExists)
                {
                    var all = mainRepository.GetCountOfUsers();
                    var active = mainRepository.GetActiveUsers().Count;

                    await bot.SendTextMessageAsync(msg.Chat.Id, "📊 آمار کاربران ربات:" +
                                                                $"\n👥 کل کاربران: {all}" +
                                                                $"\n✅ کاربران فعال: {active}" +
                                                                $"\n❌ کاربران غیرفعال: {all - active}");

                }
                else
                    await bot.SendTextMessageAsync(msg.Chat.Id, "❕شما در حال حاضر عضو ربات نیستید.");
            }
            else
            {
                await bot.SendTextMessageAsync(msg.Chat.Id, "❌ دستور شما نامفهوم بود");
            }
        }

        public static async Task SendMessage(List<DataLayer.Models.User> users, List<FeedItem> items)
        {
            foreach (var user in users)
            {
                foreach (var item in items)
                {
                    var descriptionSplit = item.Description.Split(';');

                    var text = $"🔥 Title: *{item.Title}*" +
                        $"\n🖼️ Quality: *{descriptionSplit[1].Split('/').Last()}*" +
                        $"\n💾 Size: *{descriptionSplit[0]}*" +
                        $"\n\n📆 Date: {item.PublishingDate?.ToShamsi()}";
                    await BotController.bot.SendTextMessageAsync(user.Id, text, Telegram.Bot.Types.Enums.ParseMode.Markdown);
                }
            }
        }
    }
}