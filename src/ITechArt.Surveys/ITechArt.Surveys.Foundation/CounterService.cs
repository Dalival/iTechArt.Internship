using System.Linq;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Repositories;

namespace ITechArt.Surveys.Foundation
{
    public class CounterService
    {
        private readonly UnitOfWork _unitOfWork;

        public CounterService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Counter IncrementAndGetCounter()
        {
            var counter = new Counter();
            if (_unitOfWork.Counters.GetAll().Any())
            {
                counter = _unitOfWork.Counters.GetAll().First();
            }
            counter.Value++;
            _unitOfWork.Counters.Save(counter);

            return counter;
        }
    }
}