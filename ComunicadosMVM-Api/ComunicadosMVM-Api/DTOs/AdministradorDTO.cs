﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComunicadosMVM_Api.DTOs
{
    public class AdministradorDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Consecutivo { get; set; }
        public int ExternaInternaId { get; set; }
        public string Texto { get; set; }
    }
}
