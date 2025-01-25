namespace BubbleJump.Model.SuperJump
{
    public interface ISuperJumpMutableModel : ISuperJumpModel
    {
        void SetPlannedTime(long? plannedTime);
        void SetStrength(float strength);
        void SetMaxStrength(float strength);
    }
}