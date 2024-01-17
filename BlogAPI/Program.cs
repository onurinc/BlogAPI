using BlogAPI.Core;
using BlogAPI.Data;
using BlogAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();

builder.Services.AddSingleton<FileService>();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(conn))
{
    builder.Services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("ITBlogDb"));
}
else
{
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(conn));
}


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApiDbContext>();
    
    if (!context.Database.IsInMemory() && context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();