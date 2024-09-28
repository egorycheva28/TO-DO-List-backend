using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext <TodoContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});*/

app.UseHttpsRedirection();

//app.UseAuthorization();
app.MapControllers();
//app.UseDeveloperExceptionPage();
app.Run();



