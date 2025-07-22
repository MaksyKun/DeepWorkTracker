using System.Threading.Tasks;
using DeepWorkTracker.Core;
using DeepWorkTracker.Core.Contracts.Services;
using DeepWorkTracker.Core.Models;
using DeepWorkTracker.Service.Services;
using DeepWorkTracker.Web.Components.Shared;
using Microsoft.AspNetCore.Components;

namespace DeepWorkTracker.Web.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] public IDeepWorkService DeepWorkService { get; set; }
        [Inject] public DeepWorkSessionTracker SessionTracker { get; set; }
        private ICollection<DeepWorkSession> DataSource { get; set; } = [];
        private DeepWorkSession PreviousEntry { get; set; }

        protected override async Task OnInitializedAsync()
        {

            Reload();
        }

        public void Reload()
        {
            DataSource = DeepWorkService.GetAllDeepWorkSessions();
            // DataSource = DataSource.OrderByDescending(x => x.Date).ToList();
            // Manual maths
            DataSource = DeepWorkService.GetAllDeepWorkSessions().OrderByDescending(x => x.Date).ThenByDescending(x=> x.StartTime).ToList();
            foreach (var entry in DataSource)
            {
                entry.DeepWorkHours = (entry.EndTíme.ToTimeSpan() - entry.StartTime.ToTimeSpan()).TotalHours;
                entry.OutputPerHour = entry.Output / 60;
                entry.IntensityOfFocus = (entry.FinishedTasks * entry.Output) / (entry.Distractions + 1);
                entry.HighQualityWorkProduced = (entry.DeepWorkHours * entry.IntensityOfFocus) / 10;
            }
        }

        private int PageSize { get; set; } = 10;
        private int CurrentPage { get; set; } = 1;
        private int TotalPages => (int)Math.Ceiling((double)DataSource.Count / PageSize);

        private IEnumerable<DeepWorkSession> PagedData
            => DataSource
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize);

        private void GoToPage(int page)
        {
            if (page < 1) page = 1;
            if (page > TotalPages) page = TotalPages;
            CurrentPage = page;
            PreviousEntry = null; // Optionally reset
            StateHasChanged();
        }

        public (string Value, string Percentage, bool IsPositive, bool ShowPercentage) GetRateAndPercent(double current, double previous)
        {
            // Show at least "0.00" instead of blank
            string mainValue = current.ToString("0.##");

            // No previous value: only show number, not percent
            if (previous == -1 || previous == current)
                return (mainValue, "", false, false);

            // Prevent division by zero
            if (previous == 0)
            {
                // Option 1: If both are zero, don't show percent
                if (current == 0)
                    return (mainValue, "", false, false);

                // Option 2: If previous is zero but current > 0, treat as +100%
                return (mainValue, "+100%", true, true);
            }

            double percent = previous > current
                ? -((1 - (current / previous)) * 100)
                : ((1 - (previous / current)) * 100);

            return (
                mainValue,
                percent.ToString("+#0.##;-#0.##") + "%",
                percent > 0,
                true
            );
        }

        public string GetOutputUnitSymbol(DeepWorkSession session)
        {
            switch (session.OutputUnit)
            {
                case DeepWorkSession.OutputType.Codelines:
                    return "🖥️";
                case DeepWorkSession.OutputType.Words:
                    return "📑";
                case DeepWorkSession.OutputType.Executions:
                    return "❗";
                default:
                    return "";
            }
        }

        public string GetGenericRate(double current, double previous)
        {
            if (previous == -1 || previous == current) return current.ToString("#.##");
            return previous > current
            ?
            $"{current.ToString("#.##")} (-" + ((1 - (current / previous)) * 100).ToString("0.##") + "%)"
            :
            $"{current.ToString("#.##")} (+" + ((1 - (previous / current)) * 100).ToString("0.##") + "%)";
        }
        public string GetGenericRateColor(double current, double previous)
        {
            if (previous == -1 || previous == current) return "";
            return previous > current ? "color: red" : "color: green";
        }

        private string ElapsedDisplay => SessionTracker.CurrentSession?.DeepWorkHours is double h ? TimeSpan.FromHours(h).ToString(@"hh\:mm\:ss") : "00:00:00";

        public void StartDeepWorkSession()
        {
            GlobalStates.IsFocusModeEnabled = true;
            SessionTracker.OnTick += () => InvokeAsync(StateHasChanged);
            SessionTracker.StartSession();
            StateHasChanged();
        }

        public void StopDeepWorkSession()
        {
            GlobalStates.IsFocusModeEnabled = false;
            SessionTracker.OnTick -= () => InvokeAsync(StateHasChanged);
            SessionTracker.StopSession();
            if(SessionTracker.CurrentSession != null)
            {
                DeepWorkService.AddDeepWorkSession(SessionTracker.CurrentSession);
                Reload();
            }
            StateHasChanged();
        }
    }
}
