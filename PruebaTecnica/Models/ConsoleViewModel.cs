
using DataAccess;
using PruebaTecnica.DTO.Console;
using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PruebaTecnica.Models
{
    public class ConsoleViewModel
    {
        PruebaTecnicaEntities _entity = new PruebaTecnicaEntities();

        public async Task<List<ConsoleDTO>> GetConsoles()
        {
            var consoles = await _entity.Consoles.Where(data => data.is_active == true)
                                                .AsNoTracking()
                                                 .Select(data => new ConsoleDTO
                                                 {
                                                     Id = data.id,
                                                     Name = data.name,
                                                     Brand = data.Brand1.description,
                                                     ReleaseDate = data.release_date,
                                                     StorageCapacity = data.storage_capacity,
                                                     Price = data.price
                                                 })
                                                 .ToListAsync();

            return consoles;
        }


        public async Task CreateConsole(CreateConsoleDTO dto)
        {
            DateTime date;

            if (DateTime.TryParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {

                var console = new DataAccess.Console
                {
                    name = dto.Name,
                    brand_id = dto.BrandId,
                    release_date = date,
                    storage_capacity = dto.StorageCapacity,
                    price = dto.Price,
                    comment = dto.Comment,
                    user_created_id = await GetIdByUsername(dto.username),
                    created_date = DateTime.Now,
                    is_active = true
                };

                _entity.Consoles.Add(console);
                await _entity.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Invalid Date");
            }
        }

        public async Task UpdateConsole(UpdateConsoleDTO dto)
        {

            var console = await  _entity.Consoles.FindAsync(dto.Id);

            if (console == null)
            {
                throw new Exception("Invalid Console");
            }

            DateTime date;
            if (DateTime.TryParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                console.name = dto.Name;
                console.brand_id = dto.BrandId;
                console.release_date = date;
                console.storage_capacity = dto.StorageCapacity;
                console.price = dto.Price;
                console.comment = dto.Comment;
                console.modified_date = DateTime.Now;
                console.user_modified_id = await GetIdByUsername(dto.username);

                await _entity.SaveChangesAsync();
            }
        }

        public async Task DisableConsole(int id,string userName)
        {
            var console = _entity.Consoles.Where(data => data.id == id).FirstOrDefault();
            if(console == null) throw new System.Exception("Usuario Invalido");

            console.is_active = false;
            console.modified_date = DateTime.Now;
            console.user_modified_id = await GetIdByUsername(userName);

            await _entity.SaveChangesAsync();

        }

        public async Task<ConsoleDTO> GetConsoleDetails(int id)
        {
            var console = await _entity.Consoles.FindAsync(id);

            var dto = new ConsoleDTO
            {
                Id = console.id,
                Name = console.name,
                Brand = console.Brand1.description,
                ReleaseDate = console.release_date,
                StorageCapacity = console.storage_capacity,
                Price = console.price,
                Comment = console.comment,
                BrandId = console.brand_id
            };

            return dto;
        }

        public async Task<List<BrandSelector>> GetBrandSelector()
        {
            var brands = await _entity.Brands
                                      .AsNoTracking()
                                      .Select(data => new BrandSelector { 
                                          Id = data.id,
                                          Description = data.description,
                                      })
                                      .ToListAsync();

            return brands;
        }

        public async Task<int> GetIdByUsername(string username)
        {
            var usuario = await _entity.Usuarios.Where(data => data.username == username).FirstOrDefaultAsync();
            if (usuario == null) throw new System.Exception("Usuario Invalido");
            return usuario.id;
        }

    }
}