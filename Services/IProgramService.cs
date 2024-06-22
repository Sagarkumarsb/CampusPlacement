using CampusPlacement.Models;

namespace CampusPlacement.Services
{
    public interface IProgramService
    {
        Task AddProgramAsync(ProgramModel program);
        Task UpdateProgramAsync(string id, ProgramModel program);
        Task<ProgramModel> GetProgramByIdAsync(string id);
    }
}
