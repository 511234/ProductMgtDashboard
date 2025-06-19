using Microsoft.EntityFrameworkCore;
using ProductMgtDashboardBe.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Management Dashboard v1");
    });
}

// app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


// app.MapGet("/", () => "Hello World!");

app.Run();