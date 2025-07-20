using DeepWorkTracker.Core.Models;

namespace DeepWorkTracker.Core.Contracts.Services
{
    public interface IDeepWorkService
    {
        ICollection<DeepWorkSession> GetAllDeepWorkSessions();
        void AddDeepWorkSession(DeepWorkSession session);
        void UpdateDeepWorkSession(DeepWorkSession session);
        void DeleteDeepWorkSession(DeepWorkSession session);
    }
}
