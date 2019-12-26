using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Permisos.Data;
using Permisos.Data.Interfaces;
using Permisos.Web.Controllers.Api;
using System;

namespace Permisos.Tests {
    [TestClass]
    public class TipoPermisosControllerTests {
        private Mock<IUnitOfWork> _uowStub;
        private Mock<IMapper> _mapperStub;

        [TestInitialize]
        public void BeforeEach() {
            _mapperStub = new Mock<IMapper>();
            InitializeUnitOfWorkStub();

            //////

            void InitializeUnitOfWorkStub() {
                _uowStub = new Mock<IUnitOfWork>();

                var repoStub = new Mock<IRepository<TipoPermiso>>();
                _uowStub.Setup(_ => _.TipoPermisos).Returns(repoStub.Object);
            }
        }       

        [TestMethod]
        public void GetShouldCallTipoPermisoRepo() {
            // arrange
            var ctrl = new TipoPermisosController(_uowStub.Object, _mapperStub.Object);

            // act
            var results = ctrl.Get();

            // assert
            _uowStub.Verify(_ => _.TipoPermisos.Get(), 
                Times.Once, 
                "El repositorio no fue ejectudado una sola vez");
        }

    }
}
