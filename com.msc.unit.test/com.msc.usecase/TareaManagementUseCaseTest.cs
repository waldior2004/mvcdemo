using com.msc.domain.entities.Sistema;
using com.msc.usecase.Interfaces;
using com.msc.wcf.entities;
using com.msc.wcf.entities.Sistema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using security.back.usecase;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.msc.unit.test.com.msc.usecase
{
    [TestClass]
    public class TareaManagementUseCaseTest
    {
        private ITareaManagementUseCase _usecase;
        private Mock<IUnitOfWork> _unitOfWork;

        public TareaManagementUseCaseTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [TestMethod]
        public async Task ShouldReturn_Get_OK()
        {
            //Arrange
            List<TareaDTO> lstTareas= new List<TareaDTO> { 
                new TareaDTO { Id = 1, Titulo = "Tarea 1", Descripcion = "Descripcion 1", Completado = true },
                new TareaDTO { Id = 2, Titulo = "Tarea 2", Descripcion = "Descripcion 2", Completado = false },
            };
            _unitOfWork.Setup(x => x.TareaRepository.Get().Result).Returns(lstTareas);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act
            var respuesta = await _usecase.Get();

            //Assert
            respuesta.Count().ShouldBe(2);
        }

        [TestMethod]
        public async Task ShouldReturn_Get_NoItems()
        {
            //Arrange
            List<TareaDTO> lstTareas = new List<TareaDTO>();
            _unitOfWork.Setup(x => x.TareaRepository.Get().Result).Returns(lstTareas);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act
            var respuesta = await _usecase.Get();

            //Assert
            respuesta.Count().ShouldBe(0);
        }

        [TestMethod]
        public async Task ShouldReturn_GetById_OK()
        {
            //Arrange
            TareaDTO tarea = new TareaDTO { 
                Id = 1, 
                Titulo = "Tarea 1", 
                Descripcion = "Descripcion 1", 
                Completado = true 
            };
            _unitOfWork.Setup(x => x.TareaRepository.Get(1).Result).Returns(tarea);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act
            var respuesta = await _usecase.Get(1);

            //Assert
            respuesta.ShouldNotBeNull();
            respuesta.ShouldBe(tarea);
        }

        [TestMethod]
        public async Task ShouldReturn_GetById_NoItems()
        {
            //Arrange
            TareaDTO tarea = new TareaDTO();
            _unitOfWork.Setup(x => x.TareaRepository.Get(2).Result).Returns(tarea);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act
            var respuesta = await _usecase.Get(2);

            //Assert
            respuesta.ShouldNotBeNull();
            respuesta.ShouldBe(tarea);
        }

        [TestMethod]
        public async Task ShouldReturn_New_OK()
        {
            //Arrange
            List<TareaDTO> lstTareas = new List<TareaDTO> {
                new TareaDTO { Id = 1, Titulo = "Tarea 1", Descripcion = "Descripcion 1", Completado = true },
                new TareaDTO { Id = 2, Titulo = "Tarea 2", Descripcion = "Descripcion 2", Completado = false },
            };
            _unitOfWork.Setup(x => x.TareaRepository.Get().Result).Returns(lstTareas);
            Tarea tarea = new Tarea(4, "Tarea 4", "Descripcion 4", true);
            _unitOfWork.Setup(x => x.TareaRepository.Add(tarea).Result).Returns(0);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act
            var respuesta = await _usecase.New(tarea);

            //Assert
            respuesta.ShouldNotBeNull();
            respuesta.Id.ShouldBe(0);
            respuesta.Descripcion.ShouldBe("Registro Agregado Correctamente");
        }

        [TestMethod]
        public void ShouldReturn_New_Transaccion_Error()
        {
            //Arrange
            List<TareaDTO> lstTareas = new List<TareaDTO> {
                new TareaDTO { Id = 1, Titulo = "Tarea 1", Descripcion = "Descripcion 1", Completado = true },
                new TareaDTO { Id = 2, Titulo = "Tarea 2", Descripcion = "Descripcion 2", Completado = false },
            };
            _unitOfWork.Setup(x => x.TareaRepository.Get().Result).Returns(lstTareas);
            Tarea tarea = new Tarea(4, "Tarea 4", "Descripcion 4", true);
            _unitOfWork.Setup(x => x.TareaRepository.Add(tarea).Result)
                .Throws(new Exception());
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act, Assert
            Should.Throw<InvalidOperationException>(async () => await _usecase.New(tarea))
                .Message.ShouldBe("La transacción no se pudo realizar");
        }

        [TestMethod]
        public void ShouldReturn_New_TituloExiste_Error()
        {
            //Arrange
            List<TareaDTO> lstTareas = new List<TareaDTO> {
                new TareaDTO { Id = 1, Titulo = "Tarea 1", Descripcion = "Descripcion 1", Completado = true },
                new TareaDTO { Id = 4, Titulo = "Tarea 4", Descripcion = "Descripcion 4", Completado = true },
            };
            _unitOfWork.Setup(x => x.TareaRepository.Get().Result).Returns(lstTareas);
            Tarea tarea = new Tarea(4, "Tarea 4", "Descripcion 4", true);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act, Assert
            Should.Throw<ArgumentException>(async () => await _usecase.New(tarea))
                .Message.ShouldBe("El titulo ya está siendo utilizado en otra tarea");
        }

        [TestMethod]
        public async Task ShouldReturn_Edit_OK()
        {
            //Arrange
            TareaDTO existe = new TareaDTO { 
                Id = 1, 
                Titulo = "Tarea 1", 
                Descripcion = "Descripcion 1", 
                Completado = true 
            };
            _unitOfWork.Setup(x => x.TareaRepository.Get(1).Result).Returns(existe);
            Tarea input = new Tarea(1, "Tarea 2", "Descripcion 2", true);
            _unitOfWork.Setup(x => x.TareaRepository.Edit(input).Result).Returns(0);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act
            var respuesta = await _usecase.Edit(input);

            //Assert
            respuesta.ShouldNotBeNull();
            respuesta.Id.ShouldBe(0);
            respuesta.Descripcion.ShouldBe("Registro Modificado Correctamente");
        }

        [TestMethod]
        public void ShouldReturn_Edit_IdNoExiste_Error()
        {
            //Arrange
            _unitOfWork.Setup(x => x.TareaRepository.Get(1).Result).Returns(() => null);
            Tarea tarea = new Tarea(1, "Tarea 1", "Descripcion 1", true);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act, Assert
            Should.Throw<KeyNotFoundException>(async () => await _usecase.Edit(tarea))
                .Message.ShouldBe("No se encontró el registro");
        }

        [TestMethod]
        public void ShouldReturn_Edit_Transaccion_Error()
        {
            //Arrange
            TareaDTO existe = new TareaDTO
            {
                Id = 1,
                Titulo = "Tarea 1",
                Descripcion = "Descripcion 1",
                Completado = true
            };
            _unitOfWork.Setup(x => x.TareaRepository.Get(1).Result).Returns(existe);
            Tarea input = new Tarea(1, "Tarea 2", "Descripcion 2", true);
            _unitOfWork.Setup(x => x.TareaRepository.Edit(input).Result)
                .Throws(new Exception());
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act, Assert
            Should.Throw<InvalidOperationException>(async () => await _usecase.Edit(input))
                .Message.ShouldBe("La transacción no se pudo realizar");
        }

        [TestMethod]
        public async Task ShouldReturn_Delete_OK()
        {
            //Arrange
            TareaDTO existe = new TareaDTO
            {
                Id = 1,
                Titulo = "Tarea 1",
                Descripcion = "Descripcion 1",
                Completado = true
            };
            _unitOfWork.Setup(x => x.TareaRepository.Get(1).Result).Returns(existe);
            _unitOfWork.Setup(x => x.TareaRepository.Delete(1).Result).Returns(0);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act
            var respuesta = await _usecase.Delete(1);

            //Assert
            respuesta.ShouldNotBeNull();
            respuesta.Id.ShouldBe(0);
            respuesta.Descripcion.ShouldBe("Registro Eliminado Correctamente");
        }

        [TestMethod]
        public void ShouldReturn_Delete_IdNoExiste_Error()
        {
            //Arrange
            _unitOfWork.Setup(x => x.TareaRepository.Get(1).Result).Returns(() => null);
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act, Assert
            Should.Throw<KeyNotFoundException>(async () => await _usecase.Delete(1))
                .Message.ShouldBe("No se encontró el registro");
        }

        [TestMethod]
        public void ShouldReturn_Delete_Transaccion_Error()
        {
            //Arrange
            TareaDTO existe = new TareaDTO
            {
                Id = 1,
                Titulo = "Tarea 1",
                Descripcion = "Descripcion 1",
                Completado = true
            };
            _unitOfWork.Setup(x => x.TareaRepository.Get(1).Result).Returns(existe);
            _unitOfWork.Setup(x => x.TareaRepository.Delete(1).Result)
                .Throws(new Exception());
            _usecase = new TareaManagementUseCase(_unitOfWork.Object);

            //Act, Assert
            Should.Throw<InvalidOperationException>(async () => await _usecase.Delete(1))
                .Message.ShouldBe("La transacción no se pudo realizar");
        }

    }
}
