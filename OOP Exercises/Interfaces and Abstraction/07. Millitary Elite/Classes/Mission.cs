
using MilitaryElite.Enumerators;
using MilitaryElite.Interfaces;

namespace MilitaryElite.Classes
{
   public class Mission : IMission
    {
        public Mission(string missionName, MissionState state)
        {
            MissionName = missionName;
            State = state;
        }
        public string MissionName { get; }
        public MissionState State { get; private set; }

        public void CompleteMission()
        {
            State = MissionState.Finished;
        }

        public override string ToString()
        {
            return $"Code Name: {MissionName} State: {State}";
        }
    }
}
