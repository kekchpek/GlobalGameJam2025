using AsyncReactAwait.Bindable;

namespace BubbleJump.Model.SuperJump
{
    public class SuperJumpModel : ISuperJumpMutableModel
    {

        private readonly Mutable<long?> _jumpPlannedTime = new();
        private readonly Mutable<float> _strength = new();
        private readonly Mutable<float> _maxStrength = new();

        public IBindable<long?> JumpPlannedTime => _jumpPlannedTime;
        public IBindable<float> Strength => _strength;
        public IBindable<float> MaxStrength => _maxStrength;
        
        public void SetPlannedTime(long? plannedTime)
        {
            _jumpPlannedTime.Value = plannedTime;
        }

        public void SetStrength(float strength)
        {
            _strength.Value = strength;
        }

        public void SetMaxStrength(float strength)
        {
            _maxStrength.Value = strength;
        }
    }
}