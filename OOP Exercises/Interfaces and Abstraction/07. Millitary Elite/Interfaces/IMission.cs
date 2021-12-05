
using MilitaryElite.Enumerators;

namespace MilitaryElite.Interfaces
{
    public interface IMission
    {
        string MissionName { get; }

        MissionState State { get; }

        void CompleteMission();
    }
}
