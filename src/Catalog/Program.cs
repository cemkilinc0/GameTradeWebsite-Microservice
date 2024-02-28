using Microsoft.EntityFrameworkCore;
using Catalog.Repositories; 
using Catalog.Repositories.Interfaces;
using Catalog.Data;
using Catalog.Events;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogConnection")));

builder.Services.AddScoped<IGameItemRepository, GameItemRepository>();

//Setting up CORS policy 
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
        builder => builder.WithOrigins("http://localhost:8081") 
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

//Set up rabbitMQ connection
builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory() { HostName = "rabbitmq", DispatchConsumersAsync = true };
    return factory.CreateConnection();
});

builder.Services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("MyCorsPolicy");

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CatalogDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("-------" + ex);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.UseRouting(); 
app.MapControllers(); 

app.Run();

