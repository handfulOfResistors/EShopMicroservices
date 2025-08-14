var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddCarter();//hendluje minimap api endpoints
builder.Services.AddMediatR(config => //manadzuje command and query handlers
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();




app.Run();
//prvo se kreira handler pa endpoint, pa se endpoint mapira na rutu