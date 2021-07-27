using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface ICounterService
    {
        Task<Counter> IncrementAndGetCounter();
    }
}
