using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepWorkTracker.Core.Models;

namespace DeepWorkTracker.Repository
{
    public class DBInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();
            // Seed initial data if necessary
            if (!context.DeepWorkSessions.Any())
            {
                context.DeepWorkSessions.Add(new DeepWorkSession()
                {
                    Id = Guid.NewGuid(),
                    Date = DateOnly.Parse("19.07.2025"),
                    StartTime = TimeOnly.Parse("08:30:00"),
                    EndTíme = TimeOnly.Parse("09:45:00"),
                    ContextSwitches = 1,
                    FinishedTasks = 1,
                    Output = 950,
                    OutputUnit = DeepWorkSession.OutputType.Codelines,
                    FocusScore = 0.8,
                    DeepWorkHours = 1.25
                });
                context.DeepWorkSessions.Add(new DeepWorkSession()
                {
                    Id = Guid.NewGuid(),
                    Date = DateOnly.Parse("19.07.2025"),
                    StartTime = TimeOnly.Parse("13:15:00"),
                    EndTíme = TimeOnly.Parse("14:20:00"),
                    ContextSwitches = 2,
                    FinishedTasks = 1,
                    Output = 1100,
                    OutputUnit = DeepWorkSession.OutputType.Words,
                    FocusScore = 0.75,
                    DeepWorkHours = 1.05
                });
                context.DeepWorkSessions.Add(new DeepWorkSession()
                {
                    Id = Guid.NewGuid(),
                    Date = DateOnly.Parse("20.07.2025"),
                    StartTime = TimeOnly.Parse("08:30:00"),
                    EndTíme = TimeOnly.Parse("10:15:00"),
                    ContextSwitches = 1,
                    FinishedTasks = 1,
                    Output = 1300,
                    OutputUnit = DeepWorkSession.OutputType.Codelines,
                    FocusScore = 0.8,
                    DeepWorkHours = 1.75
                });
                context.SaveChanges();
            }
        }
    }
}
