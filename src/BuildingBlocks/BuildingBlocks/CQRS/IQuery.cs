using MediatR;
namespace BuildingBlocks.CQRS;
public interface IQuery<out TResponse> : IRequest<TResponse> //koristi se za operacije citanja iz baze
    where TResponse : notnull
{

}
