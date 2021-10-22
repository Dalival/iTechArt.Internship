using ITechArt.Common.Logger;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;

namespace ITechArt.Surveys.Foundation
{
    public class SurveyService : ISurveyService
    {
        private readonly ICustomLogger _logger;

        private readonly IRepository<Survey> _surveyRepository;


        public SurveyService(ICustomLogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;

            _surveyRepository = unitOfWork.GetRepository<Survey>();
        }
    }
}
