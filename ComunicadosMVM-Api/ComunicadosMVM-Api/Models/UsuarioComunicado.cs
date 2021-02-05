using System;
using System.Collections.Generic;

namespace ComunicadosMVM_Api.Models
{
    public partial class UsuarioComunicado
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Consecutivo { get; set; }
        public int ExternaInternaId { get; set; }
        public string Texto { get; set; }

        public virtual ExternaInterna ExternaInterna { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
