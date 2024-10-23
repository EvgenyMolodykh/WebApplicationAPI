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

            // Преобразуем текущую дату и время в строку
            string timestamp = currentTime.ToString("yyyyMMddHHmmss");

            // Подсчёт четных и нечетных цифр
            foreach (char c in timestamp)
            {
                if (int.TryParse(c.ToString(), out int digit))
                {
                    if (digit % 2 == 0) evenCount++;
                    else oddCount++;
                }
            }

            // Формируем ответ в зависимости от количества четных и нечетных цифр
            if (evenCount > oddCount)
                return "чет!";
            else if (oddCount > evenCount)
                return "нечет!";
            else
                return "равно!";
        }
    }
}
