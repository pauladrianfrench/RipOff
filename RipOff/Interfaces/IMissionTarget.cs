namespace RipOff
{
    public interface IMissionTarget
    {
        IEntity Target { get; }
        MissionObjective Objective { get;  }
        bool Complete { get; set; }
    }
}
