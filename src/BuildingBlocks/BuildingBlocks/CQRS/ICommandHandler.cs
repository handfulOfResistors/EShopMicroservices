 using MediatR;  

namespace BuildingBlocks.CQRS;


public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{
    //nemamo nikakav response, samo izvršavamo komandu i ne vraćamo ništa
}



public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand: ICommand<TResponse>
    where TResponse : notnull
{ //dobijamo TResponse objekat koji ne moze da bude null
}