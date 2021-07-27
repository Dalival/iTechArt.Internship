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
            var counters = await counterRepository.GetAllAsync();
            var counter = counters.FirstOrDefault();

            if (counter != null)
            {
                counter.Value++;
                counterRepository.Update(counter);
            }
            else
            {
                counter = new Counter()
                {
                    Value = 1
                };
                counterRepository.Add(counter);
            }

            await _unitOfWork.SaveAsync();

            return counter;
        }
    }
}
