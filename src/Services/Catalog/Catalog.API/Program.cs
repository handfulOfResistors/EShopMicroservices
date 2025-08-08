var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddCarter();//hendluje minimap api endpoints
builder.Services.AddMediatR(config => //manadzuje command and query handlers
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();




app.Run();
