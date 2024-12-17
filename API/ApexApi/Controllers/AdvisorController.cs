using ApexApi.Data.Repository.IRepository;
using ApexApi.Models;
using ApexApi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApexApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AdvisorController : Controller
    {
        private readonly IAdvisorRepository _repository;
        public AdvisorController(IAdvisorRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("advisors/{id?}")]
        public async Task<IActionResult> GetAdvisor(long? id)
        {
            if (id == null || id == 0)
            {
                var objAdvisorList = _repository.GetAll();
                foreach(Advisor advisor in objAdvisorList)
                {
                    advisor.SIN = advisor.SIN.MaskAllButLast(3, 'X');
                    advisor.PhoneNumber = advisor.PhoneNumber.MaskAllButLast(3, 'X');
                }
                return Json(new { advisors = objAdvisorList });
            }
            else
            {
                var objAdvisor = _repository.Get(adv => adv.Id == id);
                
                if (objAdvisor == null)
                    return NotFound(new { Status = "Error", Message = "Advisor not found!" });
                else
                {
                    objAdvisor.SIN = objAdvisor.SIN.MaskAllButLast(3, 'X');
                    objAdvisor.PhoneNumber = objAdvisor.PhoneNumber.MaskAllButLast(3, 'X');
                    return Json(new { advisor = objAdvisor });
                }
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("advisors/{id}")]
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var advisor = _repository.Get(x => x.Id == id);
                if (advisor == null)
                    return NotFound(new { Status = "Error", Message = "Advisor not found!" });
                _repository.Remove(advisor);
                _repository.Save();
                return Ok(new Response { Status = "Success", Message = "Advisor deleted successfully!" });
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("advisors")]
        [HttpPost]
        public IActionResult Create(Advisor obj)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(obj);
                _repository.Save();
                return Ok(new Response { Status = "Success", Message = "Advisor created successfully!" });
            }
            else
                return BadRequest();
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("advisors")]
        [HttpPatch]
        public IActionResult Edit(UpdateAdvisor obj)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(obj);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
