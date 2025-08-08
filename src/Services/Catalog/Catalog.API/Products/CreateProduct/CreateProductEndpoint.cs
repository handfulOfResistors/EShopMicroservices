using Catalog.API.Products.CreateProduct;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    //model koji primam preko requesta(DTO)
    public record CreateProductRequest(string Name, List<string> Category, string Description, decimal Price, string ImageFile);
    public record CreateProductResponse(Guid Id);

    //ruta koja prima zahtev, prevodi ga u command, i salje ga MediatR-u da ga obradi
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //da bi kreirali novi proizvod:
            //definisemo http post metodu koristeci carter i mapster
            //mapiramo request u command object
            //nakon toga kroz MetiatR saljemo request i mapiramo rezultat u response model

            app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);

            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create product")
                .WithDescription("Create product");
        }
    } 
}

//request.Adapt<CreateProductCommand>() → koristi Mapster da napravi CreateProductCommand.
//    zasto koristi mapster kad je vec to isti tip objekta?
//sender.Send(command) → koristi MediatR da pošalje command odgovarajućem handleru (CreateProductCommandHandler). 
//    Da li je u tom slucaju handler kuvar?
//Handler vraća CreateProductResult.
//    zasto vraca CreateProductResult a ne samo Ok()
//Mapster ga pretvara u CreateProductResponse (odgovor za korisnika).
//    zasto prilagodjava odgovor? 