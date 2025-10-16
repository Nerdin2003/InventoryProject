using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data; // or wherever InventoryContext is defined
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<InventoryContext>(options => 
    options.UseSqlite(builder.Configuration. GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddCors(options => 
    options.AddPolicy("AllowAllOrigins",
    builder => {
        builder.AllowAnyOrigins()
        .AllowAnyMethod()
        .AllowAnyHeader();
    }
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

