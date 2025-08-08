using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    
    //CreateProductCommand objekat koji se salje MediatR da izvrsi odredjenu akciju (kreiranje proizvoda)
    public record CreateProductCommand(string Name,string Description,decimal Price, string ImageFile,List<string> Category) 
        : ICommand<CreateProductResult>;



    //represents the response object in Command Query Object in CQRS and MediatR request lifecycle architecture
    public record  CreateProductResult(Guid Id);
    //sadrzi biznis logiku za taj command
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async  Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //businees logic to create a product

            
            //1. create product entity from command object
            var product = new Product
            {
                
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
                
            };


            //TODO
            //2. save the database

            //3. return CreateProductResult result
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
