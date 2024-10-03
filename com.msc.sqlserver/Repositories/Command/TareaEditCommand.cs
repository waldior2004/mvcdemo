using com.msc.domain.entities.Sistema;
using MediatR;

namespace com.msc.sqlserver.Repositories.Command
{
    public class TareaEditCommand : IRequest<int>
    {
        public Tarea input { get; set; }

        public TareaEditCommand(Tarea _input)
        {
            input = _input;
        }
    }
}
