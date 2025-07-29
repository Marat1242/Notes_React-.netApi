using NotesPetProject.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<NotesDbContext>();


var app = builder.Build();

using var scope = app.Services.CreateScope();

 using var dbContext = scope.ServiceProvider.GetRequiredService<NotesDbContext>();

await dbContext.Database.EnsureCreatedAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapGet("/", () => "KUTAK");
app.Run();
