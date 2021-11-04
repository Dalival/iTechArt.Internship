using System.Threading.Tasks;
using ITechArt.Surveys.DomainModel;

namespace ITechArt.Surveys.Foundation.Interfaces
{
    public interface ISurveyService
    {
        Task<bool> CreateAsync(Survey survey);
    }
}
