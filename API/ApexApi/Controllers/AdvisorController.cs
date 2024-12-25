using ApexApi.Data.Repository.IRepository;
using ApexApi.Models;
using ApexApi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Collections;


namespace ApexApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AdvisorController : Controller
    {
        private readonly IAdvisorRepository _repository;
        private readonly ILogger<AdvisorController> _logger;
        public AdvisorController(IAdvisorRepository repository, ILogger<AdvisorController> logger)
        {
            _repository = repository;
            _logger = logger;
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
                    advisor.sin = advisor.sin.MaskAllButLast(3, 'X');
                    advisor.phoneNumber = advisor.phoneNumber?.MaskAllButLast(3, 'X');
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
                    objAdvisor.sin = objAdvisor.sin.MaskAllButLast(3, 'X');
                    objAdvisor.phoneNumber = objAdvisor.phoneNumber?.MaskAllButLast(3, 'X');
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
        public IActionResult Create(RequestObject obj)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(obj.advisor);
                IEnumerable<RuleViolation> rules= _repository.GetRuleViolations();
                if (rules.Count() > 0)
                {
                    foreach (RuleViolation rule in rules) {
                        ModelState.AddModelError(rule.PropertyName, rule.ErrorMessage);
                    }
                    var errors = new Hashtable();
                    foreach (var pair in ModelState)
                    {
                        if (pair.Value.Errors.Count > 0)
                        {
                            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                        }
                    }
                    return BadRequest(new {errors=errors});
                }
                else
                    _repository.Save();
                return Json( obj );
            }
            else
                return BadRequest();
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("advisors/{id?}")]
        [HttpPut]
        public IActionResult Edit(RequestObject obj,long id)
        {
            //Temp hack to get around ember not sending id's
            obj.advisor.Id = id;
            
            if (ModelState.IsValid)
            {
                _repository.Update(obj.advisor);
                _repository.Save();
                return Json(obj);
            }
            else
                return BadRequest();
        }
    }
}
