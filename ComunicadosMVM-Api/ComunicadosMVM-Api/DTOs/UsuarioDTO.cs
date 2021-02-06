using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComunicadosMVM_Api.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? IdRol { get; set; }

        public List<UsuarioComunicadoDTO> UsuarioComunicado { get; set; }
    }
}
