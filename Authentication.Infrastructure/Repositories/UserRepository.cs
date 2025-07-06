using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.DbEntity;
using Authentication.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserEntity>> GetRegistrationDataAsync()
        {
            var userList = await _context.Users
                .Select(u => new UserEntity 
                { 
                    Id = u.Id, 
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNum = u.PhoneNumber,
                    

                }).ToListAsync();
            return userList;

        }

        public async Task<UserEntity?> UserLoginAsync(string email, string password)
        {
            // Step 1: Fetch the user (from DB)
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            // Step 2: If not found, return empty
            if (user == null)
                return null;

            // Step 3: Verify password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid)
                return null;

            // Step 4: Return role name or generate token
            return Mapper.MapToUserEntity(user);


        }

        public async Task<string> UserRegistrationAsync(RegistrationDto registrationDto)
        {
            var user = Mapper.MapToUsers(registrationDto);
            await _context.Users.AddAsync(user);
            var res = await _context.SaveChangesAsync();
            
            if(res > 0)
            {
                return user.Id.ToString();
            }
            return string.Empty;

        }
    }
}
