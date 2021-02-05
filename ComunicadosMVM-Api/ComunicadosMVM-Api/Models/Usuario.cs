using System;
using System.Collections.Generic;

namespace ComunicadosMVM_Api.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioComunicado = new HashSet<UsuarioComunicado>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? IdRol { get; set; }

        public virtual ICollection<UsuarioComunicado> UsuarioComunicado { get; set; }
    }
}
