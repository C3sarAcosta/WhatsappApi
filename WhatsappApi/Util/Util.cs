namespace WhatsappApi.Util
{
    public class Util : IUtil
    {
        public object TextMessage(string message, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "text",
                text = new
                {
                    body = message
                }
            };
        }

        public object ImageMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "image",
                image = new
                {
                    link = url
                }
            };
        }

        public object AudioMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "audio",
                audio = new
                {
                    link = url
                }
            };
        }

        public object VideoMessage(string url, string caption, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "video",
                video = new
                {
                    link = url,
                    Caption = caption
                }
            };
        }

        public object DocumentMessage(string url, string caption, string filename, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "document",
                document = new
                {
                    link = url,
                    caption = caption,
                    filename = filename,
                }
            };
        }

        public object LocationMessage(string latitude, string longitude, string name, string address, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "location",
                location = new
                {
                    latitude = latitude,
                    longitude = longitude,
                    name = name,
                    address = address
                }
            };
        }

        public object ButtonsMessage(string text, string boton1, string boton2, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new
                    {
                        text = text
                    },
                    action = new
                    {
                        buttons = new List<object> 
                        {
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "001",
                                    tittle = boton1
                                },
                            },
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "002",
                                    tittle = boton2
                                },
                            }
                        }
                    }
                }
            };
        }
    }
}
