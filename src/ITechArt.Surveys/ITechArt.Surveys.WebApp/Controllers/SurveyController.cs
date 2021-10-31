using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;
using ITechArt.Surveys.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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
            // {
            //     Questions = new List<QuestionViewModel> {new QuestionViewModel
            //     {
            //         Title = "gggg",
            //         Description = "descr",
            //         Index= 0,
            //         Type = QuestionType.Text
            //     }}
            // };

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
                    Index = q.Index,
                    Type = q.Type
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

        public IActionResult AddQuestion(int index)
        {
            var model = new QuestionViewModel
            {
                Index = index
            };

            return PartialView("_TextQuestion", model);
        }
    }
}
