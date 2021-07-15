﻿using System.Linq;
using System.Threading.Tasks;
using ITechArt.Repositories;
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
            var counterList = await counterRepository.GetAllAsync();
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
            await _unitOfWork.CommitAsync();

            return counter;
        }
    }
}
