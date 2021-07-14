using System.Linq;
using ITechArt.Repositories;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Foundation
{
    public class CounterService
    {
        private readonly IUnitOfWork _unitOfWork;

        
        public CounterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
        public Counter IncrementAndGetCounter()
        {
            var counterRepository = _unitOfWork.GetRepository<Counter>();
            var counter = new Counter();
            if (counterRepository.GetAll().Any())
            {
                counter = counterRepository.GetAll().First();
            }
            else
            {
                counterRepository.Add(counter);
            }
            counter.Value++;
            counterRepository.Update(counter);
            _unitOfWork.Commit();

            return counter;
        }
    }
}
