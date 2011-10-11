namespace RipOff
{
    public class MissionTarget : IMissionTarget
    {
        bool complete;

        public MissionTarget(IEntity target, MissionObjective objective)
        {
            this.Target = target;
            this.Objective = objective;
            complete = false;
        }

        public IEntity Target { get; protected set; }
        public MissionObjective Objective { get; protected set; }
        public bool Complete { get { return Target.Expired || complete; } set { complete = value; } }
    }
}
