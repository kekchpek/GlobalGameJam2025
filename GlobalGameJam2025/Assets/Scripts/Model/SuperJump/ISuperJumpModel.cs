using AsyncReactAwait.Bindable;

namespace BubbleJump.Model.SuperJump
{
    public interface ISuperJumpModel
    {
        IBindable<long?> JumpPlannedTime { get; }
        IBindable<float> Strength { get; }
        IBindable<float> MaxStrength { get; }
    }
}