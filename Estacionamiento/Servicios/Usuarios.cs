﻿using System.Security.Claims;

namespace Estacionamiento.Servicios
{
    public class Usuarios : IUsuarios
    {

        private readonly HttpContext httpContext;
        public Usuarios(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public int ObtenerUsuarioId()
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var idClaim = httpContext.User.Claims.Where(x => x.Type ==ClaimTypes.NameIdentifier).FirstOrDefault();
                var id = int.Parse(idClaim.Value);
                return id;
            }
            else {
                throw new ApplicationException("El usuario no esta autenticado");
            }
            return 1;
        }

    }
}
