using Domain.EntitiesLayer;
using Infrastructure.RepositoriesLayer.InterfazLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {

        }
        public DbSet<VideoJuego> Videojuegos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

       
        }

        // Método para ejecutar procedimientos almacenados
        public async Task<List<VideoJuego>> GetAllVideojuegosAsync()
        {
            return await Videojuegos
                .FromSqlRaw("EXEC sp_GetAllVideojuegos")
                .ToListAsync();
        }

        public async Task<VideoJuego> GetVideojuegoByIdAsync(int id)
        {
            return await Videojuegos
                .FromSqlRaw("EXEC sp_GetVideojuegoById @Id = {0}", id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateVideojuegoAsync(string nombre, string compania, int anio, decimal precio)
        {
            return await Database
                .ExecuteSqlRawAsync("EXEC sp_CreateVideojuego @Nombre = {0}, @Compania = {1}, @Anio = {2}, @Precio = {3}",
                    nombre, compania, anio, precio);
        }

        public async Task<int> UpdateVideojuegoAsync(int id, string nombre, string compania, int anio, decimal precio)
        {
            return await Database
                .ExecuteSqlRawAsync("EXEC sp_UpdateVideojuego @Id = {0}, @Nombre = {1}, @Compania = {2}, @Anio = {3}, @Precio = {4}",
                    id, nombre, compania, anio, precio);
        }

        public async Task<int> DeleteVideojuegoAsync(int id)
        {
            return await Database
                .ExecuteSqlRawAsync("EXEC sp_DeleteVideojuego @Id = {0}", id);
        }


    }


}
