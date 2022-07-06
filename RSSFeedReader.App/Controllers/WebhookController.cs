using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.App.Webhook;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace RSSFeedReader.App.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update)
        {
            await handleUpdateService.EchoAsync(update);
            return Ok();

        }
    }
}
