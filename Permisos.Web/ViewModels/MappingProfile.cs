using AutoMapper;
using Permisos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permisos.Web.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            ConfigureMappingProfile();
        }

        private void ConfigureMappingProfile() {
            CreateMap<TipoPermiso, TipoPermisoVM>().ReverseMap();
            CreateMap<Permiso, PermisoVM>().ReverseMap();
        }
    }
}
