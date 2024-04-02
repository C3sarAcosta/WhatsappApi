using Microsoft.AspNetCore.Mvc;
using WhatsappApi.Models.WhatsappCloud;
using WhatsappApi.Services.WhatsappCloud.SendMessage;
using WhatsappApi.Util;

namespace WhatsappApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : Controller
    {
        private readonly IWhatsappCloudSendMessage whatsappCloudSendMessage;
        private readonly IUtil util;
        public WhatsAppController(IWhatsappCloudSendMessage whatsappCloudSendMessage, IUtil util)
        {
            this.whatsappCloudSendMessage = whatsappCloudSendMessage;
            this.util = util;
        }

        /*[HttpGet("test")]
        public async Task<IActionResult> Sample()
        {
            util.TextMessage("Esta es una prueba", "526391559933");

            var result = await whatsappCloudSendMessage.Execute(data);

            return Ok("ok sample");
        }*/

        [HttpGet]
        public IActionResult VerifyToken()
        {
            string accesToken = "gbjh54SKHvxsKJBXksc4CS4sckhjDSCdsv855sd";

            var token = Request.Query["hub.verify_token"].ToString();
            var challenge = Request.Query["hub.challenge"].ToString();

            if (challenge != null && token != null && token == accesToken)
            {
                return Ok(challenge);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReceivedMessage([FromBody] WhatsAppCloudModel body)
        {
            try
            {
                var message = body.Entry[0]?.Changes[0]?.Value?.Messages[0];
                if (message != null)
                {
                    var userNumber = message.From;
                    var userText = GetUserText(message);

                    List<object> listobjectMessage = new List<object>();

                    if(userText.ToUpper().Contains("HOLA"))
                    {
                        var objectMessage = util.TextMessage("Hola, ¿Cómo te pudo ayudar", userNumber);
                        listobjectMessage.Add(objectMessage);
                    }

                    foreach (var item in listobjectMessage)
                    {
                        await whatsappCloudSendMessage.Execute(item);
                    }

                    /*switch (userText.ToUpper())
                    {
                        case "TEXT":
                            objectMessage = util.TextMessage("Este es un mensaje de prueba", userNumber);
                            break;
                        case "IMAGE":
                            objectMessage = util.ImageMessage("https://www.delicias.tecnm.mx/img/logotipos/header/logo-tec-delicias.png", userNumber);
                            break;
                        case "AUDIO":
                            objectMessage = util.ImageMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/audio_whatsapp.mp3", userNumber);
                            break;
                        case "DOCUMENT":
                            objectMessage = util.DocumentMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/document_whatsapp.pdf", "Ejemplo PDF", "Documento de prueba", userNumber);
                            break;
                        case "VIDEO":
                            objectMessage = util.VideoMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/video_whatsapp.mp4", "Ejemplo video",  userNumber);
                            break;
                        case "LOCATION":
                            objectMessage = util.LocationMessage("28.178074651683133", "-105.47350110003156", "El granero restaurante", "Av Rio San Pedro Sur 1227, Sur 1, 33076 Delicias, Chih.", userNumber);
                            break;
                        case "BUTTONS":
                            objectMessage = util.ButtonsMessage("¿Confirmas tu registro?", "Si", "No", userNumber);
                            break;
                        default:
                            objectMessage = util.TextMessage("Lo siento no puedo entenderte", userNumber);
                            break;
                    }*/

                    //await whatsappCloudSendMessage.Execute(objectMessage);
                }

                return Ok("EVENT_RECEIVED");
            }
            catch (Exception ex)
            {
                return Ok("EVENT_RECEIVED");
            }
        }

        private string GetUserText(Message message)
        {
            string typeMessage = message.Type;
            if (typeMessage!.ToUpper() == "TEXT")
            {
                return message.Text!.Body!;
            }
            else if (typeMessage.ToUpper() == "INTERACTIVE")
            {
                string interactiveType = message.Interactive!.Type!;

                if (interactiveType.ToUpper() == "LIST_REPLY")
                {
                    return message.Interactive.List_Reply!.Title!;
                }
                else if (interactiveType.ToUpper() == "BUTTON_REPLY")
                {
                    return message.Interactive.Button_Reply!.Title!;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
