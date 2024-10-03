using Microsoft.AspNetCore.Mvc;
using MVCGAMES.Models;

namespace MVCGAMES.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("/usuarios/registro", model);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Usuario registrado con éxito";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Error al registrar el usuario");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("api/login", model);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    // Guardar token, redirigir a página segura
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error en la autenticación");
                }
            }

            return RedirectToAction("Index", "Videojuegos");
        }
    }
}
