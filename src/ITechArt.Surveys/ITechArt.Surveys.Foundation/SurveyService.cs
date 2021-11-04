using System;
using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Common.Result;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;

namespace ITechArt.Surveys.Foundation
{
    public class SurveyService : ISurveyService
    {
        private readonly ICustomLogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Survey> _surveyRepository;


        public SurveyService(ICustomLogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

            _surveyRepository = unitOfWork.GetRepository<Survey>();
        }

        public async Task<bool> CreateAsync(Survey survey)
        {
            survey.CreationDate = DateTime.Now;
            _surveyRepository.Add(survey);
            await _unitOfWork.SaveAsync();

            return true;
        }


        private OperationResult<SurveyCreationError> ValidateSurvey(Survey survey)
        {
            throw new NotImplementedException();
        }
    }
}
