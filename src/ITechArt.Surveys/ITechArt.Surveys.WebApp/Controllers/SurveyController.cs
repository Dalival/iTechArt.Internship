using ITechArt.Surveys.Foundation.Interfaces;
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
        }
    }
}
