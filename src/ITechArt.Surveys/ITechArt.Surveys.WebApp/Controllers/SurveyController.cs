using System;
using System.Linq;
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
        private readonly UserManager<User> _userManager;


        public SurveyController(ISurveyService surveyService, UserManager<User> userManager)
        {
            _surveyService = surveyService;
            _userManager = userManager;
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

            var currentUser = await _userManager.GetUserAsync(this.User);
            var survey = new Survey
            {
                Name = model.Name,
                Owner = currentUser,
                CreationDate = DateTime.Now,
                Questions = questions
            };

            await _surveyService.CreateAsync(survey);

            return RedirectToAction("Index", "Home");
        }
    }
}
