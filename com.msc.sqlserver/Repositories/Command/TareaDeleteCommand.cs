using MediatR;

namespace com.msc.sqlserver.Repositories.Command
{
    public class TareaDeleteCommand : IRequest<int>
    {
        public int id { get; set; }

        public TareaDeleteCommand(int _id)
        {
            id = _id;
        }
    }
}
