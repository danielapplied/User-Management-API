using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> users = new();

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await Task.FromResult(Ok(users));
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
              throw new KeyNotFoundException("User not found");

            return await Task.FromResult(Ok(user));
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            user.Id = users.Count + 1;
            users.Add(user);

            return await Task.FromResult(
                CreatedAtAction(nameof(GetUser), new { id = user.Id }, user)
            );
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
              throw new KeyNotFoundException("User not found");

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.Department = updatedUser.Department;

            return await Task.FromResult(NoContent());
        }

        // PUT: api/users/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
              throw new KeyNotFoundException("User not found");

            if (!string.IsNullOrEmpty(updatedUser.FullName))
               user.FullName = updatedUser.FullName;

            if (!string.IsNullOrEmpty(updatedUser.Email))
                user.Email = updatedUser.Email;

            if (!string.IsNullOrEmpty(updatedUser.Department))
               user.Department = updatedUser.Department;

            return await Task.FromResult(NoContent());
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
           if (user == null)
              throw new KeyNotFoundException("User not found");

            users.Remove(user);
            return await Task.FromResult(NoContent());
        }
    }
}