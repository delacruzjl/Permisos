using AutoMapper;
using Permisos.Data;

namespace Permisos.Web.ViewModels {
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
