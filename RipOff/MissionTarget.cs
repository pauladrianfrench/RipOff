namespace RipOff
{
    public class MissionTarget : IMissionTarget
    {
        public MissionTarget(IEntity target, MissionObjective objective)
        {
            this.Target = target;
            this.Objective = objective;
            this.Complete = false;
        }

        public IEntity Target { get; protected set; }
        public MissionObjective Objective { get; protected set; }
        public bool Complete { get; set; }
    }
}
