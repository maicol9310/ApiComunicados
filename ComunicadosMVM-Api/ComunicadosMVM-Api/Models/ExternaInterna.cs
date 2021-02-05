using System;
using System.Collections.Generic;

namespace ComunicadosMVM_Api.Models
{
    public partial class ExternaInterna
    {
        public ExternaInterna()
        {
            UsuarioComunicado = new HashSet<UsuarioComunicado>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UsuarioComunicado> UsuarioComunicado { get; set; }
    }
}
