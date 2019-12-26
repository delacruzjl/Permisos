using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Permisos.Data.Interfaces;
using Permisos.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Permisos.Web.Controllers.Api {
    [Route("api/[Controller]"), EnableCors("defaultPolicy")]
    public class TipoPermisosController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TipoPermisosController(IUnitOfWork uow, IMapper mapper) {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<TipoPermisoVM> Get() {
            return _uow.TipoPermisos.Get()
                .Select(_mapper.Map<TipoPermisoVM>);
        }
    }
}
