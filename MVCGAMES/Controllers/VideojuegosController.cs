using Microsoft.AspNetCore.Mvc;
using MVCGAMES.Models;
using System.Net.Http;
using System.Text.Json;


namespace MVCGAMES.Controllers
{
    public class VideojuegosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7155/api/videojuegos"; 
        public VideojuegosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5, string filter = "")
        {
            var url = $"{_baseUrl}?page={page}&pageSize={pageSize}&filter={filter}"; // Añade parámetros de paginación y filtro
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var videojuegos = JsonSerializer.Deserialize<IEnumerable<VideoJuegoViewModel>>(jsonString);
                return View(videojuegos);
            }

            TempData["ErrorMessage"] = "Error al obtener los videojuegos.";
            return View(new List<VideoJuegoViewModel>());
        }


        // Crear un videojuego
        [HttpPost]
        public async Task<IActionResult> Create(VideoJuegoViewModel videojuego)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Datos inválidos.";
                return RedirectToAction("Index");
            }

            var response = await _httpClient.PostAsJsonAsync(_baseUrl, videojuego);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Videojuego creado exitosamente.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Error al crear el videojuego.";
            return RedirectToAction("Index");
        }

        // Actualizar un videojuego
        [HttpPost]
        public async Task<IActionResult> Update(VideoJuegoViewModel videojuego)
        {
            // Validar y consumir el endpoint para actualizar
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Datos inválidos.";
                return RedirectToAction("Index");
            }

            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{videojuego.Id}", videojuego);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Videojuego actualizado exitosamente.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Error al actualizar el videojuego.";
            return RedirectToAction("Index");
        }

        // Eliminar un videojuego
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Videojuego eliminado exitosamente.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Error al eliminar el videojuego.";
            return RedirectToAction("Index");
        }

        // Ver detalle de un videojuego
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var videojuegos = JsonSerializer.Deserialize<IEnumerable<VideoJuegoViewModel>>(jsonString);
                return View(videojuegos);
            }

            TempData["ErrorMessage"] = "No se pudo obtener el detalle del videojuego.";
            return PartialView("_DetailsModal", new VideoJuegoViewModel());
        }


    }
}
