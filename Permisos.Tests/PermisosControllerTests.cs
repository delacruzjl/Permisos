using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Permisos.Data;
using Permisos.Data.Interfaces;
using Permisos.Web.Controllers.Api;
using Permisos.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Permisos.Tests
{
    [TestClass]
    public class PermisosControllerTests
    {
        private Mock<IUnitOfWork> _uowStub;
        private IMapper _mapper;

        [TestInitialize]
        public void BeforeEach()
        {

            InitializeMapper();
            InitializeUnitOfWorkStub();

            //////

            void InitializeMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });

                _mapper = config.CreateMapper();
            }

            void InitializeUnitOfWorkStub()
            {
                _uowStub = new Mock<IUnitOfWork>();

                var repoPermisoStub = new Mock<IRepository<Permiso>>();
                _uowStub.Setup(_ => _.Permisos).Returns(repoPermisoStub.Object);

                var repoTipoPermisoStub = new Mock<IRepository<TipoPermiso>>();
                _uowStub.Setup(_ => _.TipoPermisos).Returns(repoTipoPermisoStub.Object);
            }
        }

        [TestMethod]
        public void GetWhenExecutedShouldCallTipoPermisoRepo()
        {
            // arrange
            var ctrl = new PermisosController(_uowStub.Object, _mapper);

            // act
            var results = ctrl.Get();

            // assert
            _uowStub.Verify(_ => _.Permisos.Get(),
                Times.Once,
                "El repositorio no fue ejectudado una sola vez");


        }

        [TestMethod]
        public async Task AddWhenProvidedCorrectInfoShouldAddPermiso()
        {
            // arrange
            _uowStub.Setup(_ => _.TipoPermisos.Find(It.IsAny<Expression<Func<TipoPermiso, bool>>>()))
                .Returns(new[] { new TipoPermiso() }.AsQueryable());
            _uowStub.Setup(_ => _.CommitAsync())
                .ReturnsAsync(true);

            var ctrl = new PermisosController(_uowStub.Object, _mapper);
            var permiso = new PermisoVM
            {
                NombreEmpleado = "xyz",
                ApellidosEmpleado = "abc",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Now
            };

            // act 
            var results = await ctrl.Add(permiso);

            // assert
            Assert.IsInstanceOfType(results, typeof(CreatedResult));
            _uowStub.Verify(_ => _.CommitAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddWhenCouldntCommitShouldReturnBadRequest()
        {
            // arrange
            _uowStub.Setup(_ => _.TipoPermisos.Find(It.IsAny<Expression<Func<TipoPermiso, bool>>>()))
                .Returns(new[] { new TipoPermiso() }.AsQueryable());
            _uowStub.Setup(_ => _.CommitAsync())
                .ReturnsAsync(false);

            var ctrl = new PermisosController(_uowStub.Object, _mapper);
            var permiso = new PermisoVM
            {
                NombreEmpleado = "xyz",
                ApellidosEmpleado = "abc",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Now
            };

            // act 
            var results = await ctrl.Add(permiso);

            // assert
            Assert.IsInstanceOfType(results, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AddWhenProvidedInvalidTipoPermisoShouldReturnBadRequest()
        {
            // arrange
            _uowStub.Setup(_ => _.TipoPermisos.Find(It.IsAny<Expression<Func<TipoPermiso, bool>>>()))
                .Returns(Array.Empty<TipoPermiso>().AsQueryable());

            var ctrl = new PermisosController(_uowStub.Object, _mapper);
            var permiso = new PermisoVM
            {
                NombreEmpleado = "xyz",
                ApellidosEmpleado = "abc",
                TipoPermisoId = -1,
                FechaPermiso = DateTime.Now
            };

            // act 
            var results = await ctrl.Add(permiso);

            // assert
            Assert.IsInstanceOfType(results, typeof(BadRequestObjectResult));
            _uowStub.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [TestMethod]
        public async Task AddWhenMissingPropertiesShouldReturnBadRequest()
        {
            // arrange
            var ctrl = new PermisosController(_uowStub.Object, _mapper);
            var permiso = new PermisoVM
            {
                NombreEmpleado = null,
                ApellidosEmpleado = "abc",
                TipoPermisoId = -1,
                FechaPermiso = DateTime.Now
            };

            // act
            ctrl.ModelState.AddModelError("NombreEmpleado", "Region is mandatory");
            var results = await ctrl.Add(permiso);

            // assert
            Assert.IsInstanceOfType(results, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task RemoveWhenIdNotProvidedShouldReturnBadRequest()
        {
            // arrange
            var ctrl = new PermisosController(_uowStub.Object, _mapper);
            var fakeId = 0;

            // act 
            var results = await ctrl.Remove(fakeId);

            // assert
            Assert.IsInstanceOfType(results, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task RemoveWhenPermisoNotFoundShouldReturnBadRequest()
        {
            // arrange
            var ctrl = new PermisosController(_uowStub.Object, _mapper);
            var fakeId = 999;
            _uowStub.Setup(_ => _.Permisos.Find(It.IsAny<Expression<Func<Permiso, bool>>>()))
                .Returns(Array.Empty<Permiso>().AsQueryable());

            // act 
            var results = await ctrl.Remove(fakeId);

            // assert
            Assert.IsInstanceOfType(results, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task RemoveWhenIdFoundShouldCallRemoveFromRepo()
        {
            // arrange
            var ctrl = new PermisosController(_uowStub.Object, _mapper);
            var fakeId = 999;
            _uowStub.Setup(_ => _.Permisos.Find(It.IsAny<Expression<Func<Permiso, bool>>>()))
               .Returns(new[] { new Permiso() }.AsQueryable());
            _uowStub.Setup(_ => _.CommitAsync())
                .ReturnsAsync(true);

            // act 
            var results = await ctrl.Remove(fakeId);

            // assert
            _uowStub.Verify(_ => _.Permisos.Remove(It.IsAny<Permiso>()), Times.Once);
            Assert.IsInstanceOfType(results, typeof(OkResult));
        }

        [TestMethod]
        public async Task RemoveWhenIdFoundShouldCallCommit()
        {
            // arrange
            var ctrl = new PermisosController(_uowStub.Object, _mapper);
            var fakeId = 999;
            _uowStub.Setup(_ => _.Permisos.Find(It.IsAny<Expression<Func<Permiso, bool>>>()))
              .Returns(new[] { new Permiso() }.AsQueryable());
            _uowStub.Setup(_ => _.CommitAsync())
                .ReturnsAsync(true);

            // act 
            var results = await ctrl.Remove(fakeId);

            // assert
            _uowStub.Verify(_ => _.CommitAsync(), Times.Once);
            Assert.IsInstanceOfType(results, typeof(OkResult));
        }
    }
}
