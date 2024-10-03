using com.msc.wcf.entities.Sistema;
using MediatR;

namespace com.msc.sqlserver.Repositories.Querys
{
    public class TareaByIdQuery : IRequest<TareaDTO>
    {
        public int Id { get; set; }

        public TareaByIdQuery(int _id)
        {
            Id = _id;
        }
    }
}
