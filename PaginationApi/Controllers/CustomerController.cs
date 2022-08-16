using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginationApi.Context;
using PaginationApi.Helpers;
using PaginationApi.Models;
using PaginationApi.Services;
using PaginationApi.Wrappers;

namespace PaginationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IUriService uriService;
        public CustomerController(ApplicationDbContext context, IUriService uriService)
        {
            this.context = context;
            this.uriService = uriService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await context.Users
                .OrderBy(u =>u.id)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await context.Users.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<User>(pagedData, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await context.Users.Where(a => a.id == id).FirstOrDefaultAsync();
            return Ok(new Response<User>(user));
        }
    }
}
