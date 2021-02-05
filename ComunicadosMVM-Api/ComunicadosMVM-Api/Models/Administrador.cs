using System;
using System.Collections.Generic;

namespace ComunicadosMVM_Api.Models
{
    public partial class Administrador
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? IdRol { get; set; }
    }
}
