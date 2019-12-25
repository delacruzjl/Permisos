using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Permisos.Data;
using Permisos.Data.Interfaces;
using Permisos.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permisos.Web.Controllers.Api {
    [Route("api/[Controller]"), EnableCors("defaultPolicy")]
    public class PermisosController : Controller {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PermisosController(IUnitOfWork uow, IMapper mapper) {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]public IEnumerable<PermisoVM> Get() {
            return _uow.Permisos.Get()
                .Include(_ => _.TipoPermiso)
                .Select(_mapper.Map<PermisoVM>);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]PermisoVM vm) {
            try {
                ValidateRequiredProperties();
                var entity = await ConvertVMToEntity(vm);
                entity = await SaveEntityToDb(entity);

                return Created(
                    new Uri($"{Request?.Path}/{entity.Id}", UriKind.Relative),
                    _mapper.Map<PermisoVM>(entity));
            } catch (ArgumentException) {
                return BadRequest(ModelState);
            }
        }

       

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id) {
            ValidatePermisoExists(id);
            var entity = _uow.Permisos
                .Find(_ => _.Id == id)
                .SingleOrDefault();

            ValidatePermisoExists(entity);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _uow.Permisos.Remove(entity);
            var result = await _uow.CommitAsync();
            return result ? (IActionResult)Ok() : BadRequest("No hubo datos que borrar");
        }


        //////

        private void ValidateRequiredProperties() {
            if (ModelState.IsValid) {
                return;
            }

            const string errorMessage = "Todos los atributos del permiso son necesarios";
            SetModelStateInvalid(string.Empty, errorMessage);
            throw new ArgumentException(errorMessage);
        }

        private async Task<Permiso> ConvertVMToEntity(PermisoVM vm) {
            var entity = _mapper.Map<Permiso>(vm);
            entity = SetTipoPermisoFromDb(entity);
            
            return entity;
        }

        private Permiso SetTipoPermisoFromDb(Permiso entity) {
            entity.TipoPermiso = _uow.TipoPermisos
                .Find(_ => _.Id == entity.TipoPermisoId)
                .SingleOrDefault();

            ValidateTipoPermiso(entity);

            return entity;
        }

        private void ValidateTipoPermiso(Permiso entity) {
            if (entity.TipoPermiso != null) {
                return;
            }

            const string errorMessage = "El tipo de permiso seleccionado no existe";
            SetModelStateInvalid(string.Empty, errorMessage);
            throw new ArgumentException(errorMessage);
        }

        private async Task<Permiso> SaveEntityToDb(Permiso entity) {
            _uow.Permisos.Add(entity);
            var success = await _uow.CommitAsync();
            if (!success) {
                const string errorMessage = "No pudo salvarse la informacion en la base de datos, trate de nuevo mas tarde";
                SetModelStateInvalid(string.Empty, errorMessage);
                throw new ArgumentException(errorMessage);
            }
            return entity;
        }

        private void ValidatePermisoExists(Permiso entity) {
            if (entity != null) {
                return;
            }

            SetModelStateInvalid("id", "El permiso que intenta borrar no existe");
        }

        private void ValidatePermisoExists(int id) {
            if (id > 0) {
                return;
            }
            SetModelStateInvalid("id", "Debes proveer un id para borrar");
        }

        private void SetModelStateInvalid(string field, string errorMessage) {
            ModelState.AddModelError(field, errorMessage);
        }
    }
}
