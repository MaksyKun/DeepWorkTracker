using DeepWorkTracker.Core.Models;
namespace DeepWorkTracker.Web.Components.Shared
{
    public class DeepWorkSessionTracker
    {
        public DeepWorkSession? CurrentSession { get; private set; }
        private DateTime? _sessionStart;
        private System.Timers.Timer? _timer;

        public event Action? OnTick;

        public void StartSession()
        {
            _sessionStart = DateTime.Now;
            var now = DateTime.Now;
            CurrentSession = new DeepWorkSession
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(now),
                StartTime = TimeOnly.FromDateTime(now),
                // EndTime will be set later
            };

            _timer = new System.Timers.Timer(1000); // 1 second interval
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        private void TimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (CurrentSession == null || _sessionStart == null) return;
            var elapsed = DateTime.Now - _sessionStart.Value;
            CurrentSession.DeepWorkHours = elapsed.TotalHours;
            OnTick?.Invoke();
        }

        public void StopSession()
        {
            if (CurrentSession != null && _sessionStart != null)
            {
                var now = DateTime.Now;
                CurrentSession.EndTíme = TimeOnly.FromDateTime(now);
                CurrentSession.DeepWorkHours = (now - _sessionStart.Value).TotalHours;
            }

            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
            _sessionStart = null;
        }

        public void CancelSession()
        {
            StopSession();
            CurrentSession = null;
        }

        public void IncreaseDistractions()
        {
            if (CurrentSession != null)
            {
                CurrentSession.Distractions++;
                OnTick?.Invoke();
            }
        }

        public void IncreaseTasks()
        {
            if (CurrentSession != null)
            {
                CurrentSession.FinishedTasks++;
                OnTick?.Invoke();
            }
        }

        public void AdjustOutput(int amount)
        {
            if (CurrentSession != null)
            {
                var val = CurrentSession.Output + amount;
                CurrentSession.Output = Math.Max(0, val); // Prevent negative values (or allow if you want)
                OnTick?.Invoke();
            }
        }

        public void SetOutputUnit(DeepWorkSession.OutputType unit)
        {
            if (CurrentSession != null)
            {
                CurrentSession.OutputUnit = unit;
                OnTick?.Invoke();
            }
        }
    }

}
