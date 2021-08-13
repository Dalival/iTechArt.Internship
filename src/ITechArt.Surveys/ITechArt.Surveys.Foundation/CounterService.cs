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

            Counter counter;
            try
            {
                counter = counters.First();
                counter.Value++;
                counterRepository.Update(counter);
                _logger.LogInformation($"counter was found and incremented, new value is {counter.Value}");
            }
            catch (Exception e)
            {
                counter = new Counter()
                {
                    Value = 1
                };
                counterRepository.Add(counter);
                _logger.LogError(e, "no counters was found, a new one was created with value 1");
            }
            await _unitOfWork.SaveAsync();

            return counter;
        }
    }
}
