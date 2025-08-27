namespace Catalog.API.Products.CreateProduct
{

    //CreateProductCommand objekat koji se salje MediatR da izvrsi odredjenu akciju (kreiranje proizvoda)
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category) : ICommand<CreateProductResult>;
    //represents the response object in Command Query Object in CQRS and MediatR request lifecycle architecture
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Price must be greater than zero.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("At least one category is required.");
        }




        //sadrzi biznis logiku za taj command
        //direktno injectujemo marten IDocumentSession u handler
        internal class CreateProductCommandHandler
            (IDocumentSession session, IValidator<CreateProductCommand> validator) : ICommandHandler<CreateProductCommand, CreateProductResult>
        {
            public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var result = await validator.ValidateAsync(command, cancellationToken);
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                if (errors.Any())
                {
                    throw new ValidationException(errors.FirstOrDefault());
                }


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
}
