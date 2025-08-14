

namespace Catalog.API.Products.CreateProduct
{
    
    //CreateProductCommand objekat koji se salje MediatR da izvrsi odredjenu akciju (kreiranje proizvoda)
    public record CreateProductCommand(string Name,string Description,decimal Price, string ImageFile,List<string> Category) : ICommand<CreateProductResult>;
    //represents the response object in Command Query Object in CQRS and MediatR request lifecycle architecture
    public record  CreateProductResult(Guid Id);
    //sadrzi biznis logiku za taj command
    //direktno injectujemo marten IDocumentSession u handler
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async  Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //businees logic to create a product

            
            // creates product entity from command object
            var product = new Product
            {
                
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
                
            };


            
            session.Store(product); //saves the product entity to the database
            await session.SaveChangesAsync(cancellationToken); //saves the changes to the database
            

            //3. return CreateProductResult result
            return new CreateProductResult(product.Id);
        }
    }
}
