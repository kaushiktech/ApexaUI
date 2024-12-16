using ApexApi.Data.Repository.IRepository;
using ApexApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApexApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : Controller
    {
        private readonly IAdvisorRepository _repository;
        public AdvisorController(IAdvisorRepository repository)
        {
            _repository = repository;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("GetAdvisors")]
        public async Task<IActionResult> GetAdvisors()
        {
            var objAdvisorList = _repository.GetAll();
            return Json(new { data = objAdvisorList });
        }
    }
}
