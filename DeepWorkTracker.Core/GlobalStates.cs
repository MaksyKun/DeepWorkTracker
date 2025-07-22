using DeepWorkTracker.Core.Models;

namespace DeepWorkTracker.Core
{
    public class GlobalStates
    {
        public static bool IsFocusModeEnabled { get; set; } = false;

        public static DeepWorkSession? CurrentDeepWorkSession { get; set; } = null;
    }
}
