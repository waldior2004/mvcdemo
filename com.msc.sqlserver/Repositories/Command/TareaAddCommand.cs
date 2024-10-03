using com.msc.domain.entities.Sistema;
using MediatR;

namespace com.msc.sqlserver.Repositories.Command
{
    public class TareaAddCommand : IRequest<int>
    {
        public Tarea input { get; set; }

        public TareaAddCommand(Tarea _input)
        {
            input = _input;
        }
    }
}
