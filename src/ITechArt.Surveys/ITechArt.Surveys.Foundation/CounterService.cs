using System.Linq;
using System.Threading.Tasks;
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
        
        
        public async Task<Counter> IncrementAndGetCounter()
        {
            
            var counterRepository = _unitOfWork.GetRepository<Counter>();
            var counter = new Counter();
            var counterList = await counterRepository.GetAll();
            if (counterList.Any())
            {
                counter = counterList.First();
            }
            else
            {
                counterRepository.Add(counter);
            }
            counter.Value++;
            counterRepository.Update(counter);
            await _unitOfWork.Commit();

            return counter;
        }
    }
}
