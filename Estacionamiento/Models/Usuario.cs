namespace Estacionamiento.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public String Email { get; set; }

        public String EmailNormalizado { get; set; }

        public String PasswordHash { get; set; }
    }
}
