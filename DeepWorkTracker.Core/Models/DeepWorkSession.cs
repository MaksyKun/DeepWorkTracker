using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepWorkTracker.Core.Models
{
    public class DeepWorkSession
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTíme { get; set; }
        public int ContextSwitches { get; set; } = 0;
        public int FinishedTasks { get; set; } = 0;
        public int Output { get; set; } = 0;
        public OutputType OutputUnit { get; set; } = OutputType.Unselected;
        public double FocusScore { get; set; } = 0.0;
        public string? Notes { get; set; } = string.Empty;

        [NotMapped]
        public double? DeepWorkHours { get; set; } = 0.0;
        [NotMapped]
        public double? OutputPerHour { get; set; } = 0.0;
        [NotMapped]
        public double? IntensityOfFocus { get; set; } = 0.0;
        [NotMapped]
        public double? HighQualityWorkProduced { get; set; } = 0.0;


        public enum OutputType
        {
            Unselected = 0,
            Codelines = 100,
            Words = 200,
            Executions = 300,
        }
    }
}
