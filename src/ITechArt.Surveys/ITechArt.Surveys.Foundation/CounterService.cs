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
            var counter = new Counter();
            if (_unitOfWork.GetRepository<Counter>().SingleOrDefault() != null)
            {
                counter = _unitOfWork.GetRepository<Counter>().SingleOrDefault();
            }
            else
            {
                _unitOfWork.GetRepository<Counter>().Add(counter);
            }
            counter.Value++;
            _unitOfWork.GetRepository<Counter>().Update(counter);
            _unitOfWork.Commit();

            return counter;
        }
    }
}
