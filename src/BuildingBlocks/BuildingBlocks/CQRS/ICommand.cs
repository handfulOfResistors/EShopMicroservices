using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
        //Unit is representing Guid type for MediatR
        //ne vraca nikakav response
    }



    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
        

        // IRequest object dolazi od MediatR biblioteke
        // response je tipa TResponse
    }
}
