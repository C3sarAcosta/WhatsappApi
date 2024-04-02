namespace WhatsappApi.Services.WhatsappCloud.SendMessage
{
    public interface IWhatsappCloudSendMessage
    {
        Task<bool> Execute(object model);
    }
}
