using DeepWorkTracker.Core.Contracts.Repositories;
using DeepWorkTracker.Core.Contracts.Services;
using DeepWorkTracker.Core.Models;

namespace DeepWorkTracker.Service.Services
{
    public class DeepWorkService(IDeepWorkRepository deepWorkRepository) : IDeepWorkService
    {
        private readonly IDeepWorkRepository _deepWorkRepository = deepWorkRepository;


        public ICollection<DeepWorkSession> GetAllDeepWorkSessions()
        {
            var entries = _deepWorkRepository.GetAll().ToList();
            foreach (var entry in entries)
            {
                entry.DeepWorkHours = entry.EndTíme.ToTimeSpan().Subtract(entry.StartTime.ToTimeSpan()).Minutes / 60;
                entry.OutputPerHour = entry.Output / 60;
                entry.IntensityOfFocus = (entry.FinishedTasks * entry.Output) / (entry.ContextSwitches + 1);
                entry.HighQualityWorkProduced = entry.DeepWorkHours * entry.IntensityOfFocus;
            }
            return entries;
        }

        public void AddDeepWorkSession(DeepWorkSession session)
        {
            if (session == null) return;
            _deepWorkRepository.Insert(session);
            _deepWorkRepository.Save();
        }

        public void UpdateDeepWorkSession(DeepWorkSession session)
        {
            var entry = _deepWorkRepository.GetById(session.Id);
            if (entry == null) return;
            entry.ContextSwitches = session.ContextSwitches;
            entry.FinishedTasks = session.FinishedTasks;
            entry.Output = session.Output;
            entry.FocusScore = session.FocusScore;
            entry.Notes = session.Notes;
            _deepWorkRepository.Update(entry);
            _deepWorkRepository.Save();
        }

        public void DeleteDeepWorkSession(DeepWorkSession session)
        {
            if (session == null) return;
            _deepWorkRepository.Delete(session);
            _deepWorkRepository.Save();
        }
    }
}
