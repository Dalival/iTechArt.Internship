using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITechArt.Surveys.WebApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;


        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }


        [HttpGet]
        public IActionResult CreateSurvey()
        {
            var model = new SurveyViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey(SurveyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var questions = model.Questions.Select(q => new Question
                {
                    Title = q.Title,
                    Description = q.Description,
                    Order = q.Order,
                    Type = QuestionType.Text
                })
                .ToList();

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var survey = new Survey
            {
                Name = model.Name,
                OwnerId = currentUserId,
                CreationDate = DateTime.Now,
                Questions = questions
            };

            await _surveyService.CreateAsync(survey);

            return RedirectToAction("Index", "Home");
        }
    }
}
