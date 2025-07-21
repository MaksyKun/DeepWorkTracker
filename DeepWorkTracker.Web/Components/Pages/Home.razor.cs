using DeepWorkTracker.Core;
using DeepWorkTracker.Core.Contracts.Services;
using DeepWorkTracker.Core.Models;
using Microsoft.AspNetCore.Components;

namespace DeepWorkTracker.Web.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] public IDeepWorkService DeepWorkService { get; set; }
        private ICollection<DeepWorkSession> DataSource { get; set; } = [];
        private DeepWorkSession PreviousEntry { get; set; }

        protected override async Task OnInitializedAsync()
        {

            DataSource = DeepWorkService.GetAllDeepWorkSessions();
            // DataSource = DataSource.OrderByDescending(x => x.Date).ToList();
            // Manual maths

            foreach (var entry in DataSource)
            {
                entry.DeepWorkHours = (entry.EndTíme.ToTimeSpan() - entry.StartTime.ToTimeSpan()).TotalHours;
                entry.OutputPerHour = entry.Output / 60;
                entry.IntensityOfFocus = (entry.FinishedTasks * entry.Output) / (entry.ContextSwitches + 1);
                entry.HighQualityWorkProduced = (entry.DeepWorkHours * entry.IntensityOfFocus) / 10;
            }
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

        public void StartDeepWorkSession()
        {
            GlobalStates.IsFocusModeEnabled = true;
            StateHasChanged();
        }
    }
}
