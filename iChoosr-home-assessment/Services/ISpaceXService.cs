using iChoosr_home_assessment.PayloadModels;

namespace iChoosr_home_assessment.Services
{
    public interface ISpaceXService
    {
        Task<List<Payload>> GetAllPayloadsAsync();
        Task<Payload?> GetPayloadByIdAsync(string id);
    }
}
