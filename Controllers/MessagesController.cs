using Microsoft.AspNetCore.Mvc;
using System;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        [HttpGet("send")]
        public ActionResult<string> SendMessage()
        {
            DateTime currentTime = DateTime.Now;
            int evenCount = 0;
            int oddCount = 0;

            string timestamp = currentTime.ToString("yyyyMMddHHmmss");

            foreach (char c in timestamp)
            {
                if (int.TryParse(c.ToString(), out int digit))
                {
                    if (digit % 2 == 0) evenCount++;
                    else oddCount++;
                }
            }

            if (evenCount > oddCount)
                return "чет!";
            else if (oddCount > evenCount)
                return "нечет!";
            else
                return "равно!";
        }
    }
}
