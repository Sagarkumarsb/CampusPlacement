using CampusPlacement.Models;

namespace CampusPlacement.Services
{
    public interface ICandidateApplicationService
    {
        Task SubmitApplicationAsync(CandidateApplication application);
        Task<CandidateApplication> GetApplicationByIdAsync(int id);
    }
}
