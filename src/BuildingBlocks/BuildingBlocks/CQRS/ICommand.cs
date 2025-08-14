using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
        // koristi se kad command ne treba da vrati podatke vec samo da izvrsi akciju
        //Unit is representing Guid type for MediatR
        //ne vraca nikakav response
    }



    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
        
        // za genericke interfacee kad ima <T> i on omogucava da definisemo koji tip podataka komanda vraca
        
        
    }
}
