using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComunicadosMVM_Api.DTOs
{
    public class ExternaInternaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UsuarioComunicadoDTO> UsuarioComunicado { get; set; }
    }
}
