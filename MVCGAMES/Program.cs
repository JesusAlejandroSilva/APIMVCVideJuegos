using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
// Configurar HttpClient para consumir la API
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el middleware de la aplicación.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Usar HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Cambiar la ruta predeterminada aquí
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Cambia 'Account' y 'Login' según tu controlador y acción

app.Run();