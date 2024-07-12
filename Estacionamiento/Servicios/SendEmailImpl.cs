using Infobip.Api.Client;
using Infobip.Api.Client.Api;
using Rest;

namespace Estacionamiento.Services
{
    public class SendEmailImpl : ISendEmail 
    {
        private readonly IConfiguration configuration;

        public SendEmailImpl(IConfiguration configuration )
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(String email) {
       
        }
    }
}
