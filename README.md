<h1>Серверное приложение отправляет текстовые сообщения в клиентское приложение.</h1>
<br>
<h5>Ответное сообщение зависит от условия:</h5>

если чисел в дате и времени (с точностью до секунд), установленных на сервере, в момент отправки:
<br>
● больше четных, то отправляем сообщение «чет!»;<br>
● больше нечетных — «нечет!»;<br>
● при равном значении четных и нечетных чисел — «равно!».<br><br>
Пример на фото:

![image](https://github.com/user-attachments/assets/d25b60ef-bab2-4a5f-9ffe-49aa413cd699)

Клиентское приложение подключается к серверу по сети и начинает отображать
полученные сообщения. 

Реализации исполнения такого приложения (веб-страница,
консольное или графическое приложение) на ваше усмотрение.


Код реализации сервера контроллера сервера который вы можете скопировать к себе в проект

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

<h2>Код реализации клиента котоырй вы можете скопировать к себе в проект</h2><br>

namespace ClientApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri("https://localhost:7261/");

            while (true)
            {
                try
                {
                    var response = await client.GetStringAsync("api/messages/send");
                    Console.WriteLine($"Сообщение от сервера: {response}");

                    await Task.Delay(5000);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Ошибка при подключении к API: {e.Message}");
                    break;
                }
            }
            Console.WriteLine("Нажмите любую клавишу для выхода");
            Console.ReadLine();
        }
    }
}



