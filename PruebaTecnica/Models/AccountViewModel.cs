

using PruebaTecnica.DTO.Account;
using System.Security.Cryptography;
using System;
using System.Text;
using DataAccess;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace PruebaTecnica.Models
{
    public class AccountViewModel
    {
        PruebaTecnicaEntities _entities = new PruebaTecnicaEntities();


        public async Task CreateUser(UserRegistration dto)
        {
            string salt = GeneratorRamdonNumber(10);
            Usuario user = new Usuario
            {
                full_name = dto.FullName,
                username = dto.Username,
                email = dto.Email,
                password = HashPassword(dto.Password, salt),
                is_active = true,
                salt = salt
            };

            _entities.Usuarios.Add(user);
            await _entities.SaveChangesAsync();

        }

        public async Task<List<UsersDTO>> GetAllUser()
        {
            var users = await _entities.Usuarios.Where(data => data.is_active == true)
                                          .AsNoTracking()
                                          .Select(data => new UsersDTO{

                                              Id = data.id,
                                              UserName = data.username,
                                              Email = data.email,
                                              FullName = data.full_name,

                                           })
                                          .ToListAsync();

            return users;
        }

        public async Task<UsersDTO> GetUser(int id)
        {
            var user = await _entities.Usuarios.AsNoTracking()
                                      .FirstOrDefaultAsync(data => data.id == id && data.is_active == true);

            if (user == null) return null;

            var dto = new UsersDTO
            {
                Id = user.id,
                FullName = user.full_name,
                Email = user.email,
                UserName = user.username
            };

            return dto;
        }

        public async Task DisabledUser(int id)
        {
            var console = _entities.Usuarios.Where(data => data.id == id).FirstOrDefault();
            if (console == null) throw new System.Exception("Usuario Invalido");

            console.is_active = false;

            await _entities.SaveChangesAsync();

        }

        public bool ValidateUser(LoginRequest login)
        {
            if (!string.IsNullOrWhiteSpace(login.Username) && !string.IsNullOrWhiteSpace(login.Password)) 
            {
                var user = _entities.Usuarios.FirstOrDefault(data => data.username == login.Username && data.is_active == true);

                if (user != null)
                {
                    string hashToValidate = HashPassword(login.Password, user.salt);

                    if (user.password == hashToValidate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> CheckUsernameAvailability(string userName) 
        {
            var userTotal = await _entities.Usuarios.Where(data => data.username == userName).CountAsync();
            if (userTotal == 0) return true;
            return false;

        }

        private string HashPassword(string password, string Salt)
        {
            byte[] PasswordWithSaltBytes = Encoding.ASCII.GetBytes((password + Salt));
            using (HashAlgorithm HasGenerator = SHA256.Create())
            {
                byte[] HashComputed = HasGenerator.ComputeHash(PasswordWithSaltBytes);
                string HashString = Convert.ToBase64String(HashComputed);
                return HashString;
            }
        }

        public static string GeneratorRamdonNumber(int Length)
        {
            using (RNGCryptoServiceProvider Generator = new RNGCryptoServiceProvider())
            {
                byte[] buffer = new byte[Length];
                Generator.GetBytes(buffer);
                string Salt = Convert.ToBase64String(buffer);
                return Salt;
            }
        }

    }
}