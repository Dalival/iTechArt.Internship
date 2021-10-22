using ITechArt.Common.Logger;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;

namespace ITechArt.Surveys.Foundation
{
    public class QuestionService : IQuestionService
    {
        private readonly ICustomLogger _logger;

        private readonly IRepository<Question> _questionRepository;


        public QuestionService(ICustomLogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;

            _questionRepository = unitOfWork.GetRepository<Question>();
        }
    }
}
