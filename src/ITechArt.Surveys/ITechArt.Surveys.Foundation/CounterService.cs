using System;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.Common.Logger;
using ITechArt.Repositories.Interfaces;
using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.Foundation.Interfaces;

namespace ITechArt.Surveys.Foundation
{
    public class CounterService : ICounterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomLogger _logger;


        public CounterService(IUnitOfWork unitOfWork, ICustomLogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                _logger.LogInformation($"Counter was found and incremented, new value is {counter.Value}");
            }
            else
            {
                counter = new Counter()
                {
                    Value = 1
                };
                counterRepository.Add(counter);
                _logger.LogInformation("No counters was found, a new one was created with value 1");
            }

            await _unitOfWork.SaveAsync();

            return counter;
        }
    }
}
