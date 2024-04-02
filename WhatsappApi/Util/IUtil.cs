namespace WhatsappApi.Util
{
    public interface IUtil
    {
        object TextMessage(string message, string number);
        object ImageMessage(string url, string number);
        object AudioMessage(string url, string number);
        object VideoMessage(string url, string caption, string number);
        object DocumentMessage(string url, string caption, string filename, string number);
        object LocationMessage(string latitude, string longitude, string name, string address, string number);
        object ButtonsMessage(string text, string boton1, string boton2, string number);
    }
}
