using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Repository;
using Api.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private static readonly HttpClient HttpClient = new();
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _databaseContext;

        public UserController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        [HttpGet("{id:int}")]
        public async Task<User> GetAsync(int id)
        {
            var user = await _databaseContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user != null) user.LuckyNumber = await GetLuckyNumberAsync();
            return user;
        }

        public async Task<int> GetLuckyNumberAsync()
        {
            var url = _configuration["LuckyNumberUrl"];
            var streamTask = HttpClient.GetStreamAsync($"{url}?min=1&max=100&count=1");
            var numbers = await JsonSerializer.DeserializeAsync<List<int>>(await streamTask);

            return numbers!.First();
        }
    }
}