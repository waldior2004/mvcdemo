using com.msc.domain.entities.Sistema;
using com.msc.usecase.Interfaces;
using com.msc.wcf.entities;
using com.msc.wcf.entities.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace security.back.usecase
{
    public class TareaManagementUseCase : ITareaManagementUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public TareaManagementUseCase(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<TareaDTO>> Get()
        {
            return await unitOfWork.TareaRepository.Get();
        }

        public async Task<TareaDTO> Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El campo id debe ser un numero entero mayor a cero");
            else
            {
                var result = await unitOfWork.TareaRepository.Get(id);
                if (result == null)
                    throw new KeyNotFoundException("No se encontró el registro");
                return result;
            }
        }

        public async Task<RespuestaDTO> New(Tarea obj)
        {
            var identity = 0;
            var guid = Guid.Empty;

            var lstUsers = await unitOfWork.TareaRepository.Get();
            var exists = lstUsers.Where(p => p.Titulo == obj.Titulo).FirstOrDefault();

            if (exists == null)
            {
                try
                {
                    unitOfWork.BeginTransaction();

                    identity = await unitOfWork.TareaRepository.Add(obj);
                    unitOfWork.CommitTransaction();
                }
                catch (Exception)
                {
                    unitOfWork.RollBackTransaction();
                    throw new InvalidOperationException("La transacción no se pudo realizar");
                }
            }
            else
                throw new ArgumentException("El titulo ya está siendo utilizado en otra tarea");

            return new RespuestaDTO
            {
                Id = identity,
                Descripcion = "Registro Agregado Correctamente"
            };
        }

        public async Task<RespuestaDTO> Edit(Tarea obj)
        {
            var identity = 0;
            var exists = await unitOfWork.TareaRepository.Get(obj.Id);

            if (exists == null)
                throw new KeyNotFoundException("No se encontró el registro");
            else
            {
                try
                {
                    unitOfWork.BeginTransaction();

                    identity = await unitOfWork.TareaRepository.Edit(obj);
                    unitOfWork.CommitTransaction();
                }
                catch (Exception)
                {
                    unitOfWork.RollBackTransaction();
                    throw new InvalidOperationException("La transacción no se pudo realizar");
                }
            }

            return new RespuestaDTO
            {
                Id = identity,
                Descripcion = "Registro Modificado Correctamente"
            };
        }

        public async Task<RespuestaDTO> Delete(int id)
        {
            var identity = 0;
            var exists = await unitOfWork.TareaRepository.Get(id);

            if (exists == null)
                throw new KeyNotFoundException("No se encontró el registro");
            else
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    identity = await unitOfWork.TareaRepository.Delete(id);
                    unitOfWork.CommitTransaction();
                }
                catch (Exception)
                {
                    unitOfWork.RollBackTransaction();
                    throw new InvalidOperationException("La transacción no se pudo realizar");
                }
            }

            return new RespuestaDTO
            {
                Id = identity,
                Descripcion = "Registro Eliminado Correctamente"
            };
        }

    }
}
