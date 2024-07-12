namespace Estacionamiento.Services
{
    public interface ISendEmail
    {
        Task SendEmail(String email);
    }
}
