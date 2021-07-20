using System.Linq;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;

namespace ITechArt.Surveys.Foundation
{
    public class CounterService : ICounterService
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

            var counters = await counterRepository.GetAllAsync();
            if (counters.Any())
            {
                counter = counters.First();
                counter.Value++;
                counterRepository.Update(counter);
            }
            else
            {
                counter.Value = 1;
                counterRepository.Add(counter);
            }
            await _unitOfWork.SaveAsync();

            return counter;
        }
    }
}
