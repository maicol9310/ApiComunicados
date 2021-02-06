using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComunicadosMVM_Api.Models;

namespace ComunicadosMVM_Api.DTOs
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioDTO>()
                   .ForMember(x => x.UsuarioComunicado, o => o.Ignore())
                   //.ForMember(x => x.Nombre, o => o.MapFrom(s => s.FirstName))
                   .ReverseMap();

                cfg.CreateMap<UsuarioComunicado, UsuarioComunicadoDTO>()
                   .ReverseMap();

                cfg.CreateMap<Administrador, AdministradorDTO>()
                   .ReverseMap();


                cfg.CreateMap<ExternaInterna, ExternaInternaDTO>()
                   .ForMember(x => x.UsuarioComunicado, o => o.Ignore())
                   .ReverseMap();
            });
        }
    }
}
