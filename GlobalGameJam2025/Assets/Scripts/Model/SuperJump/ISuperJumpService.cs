using System;

namespace BubbleJump.Model.SuperJump
{
    public interface ISuperJumpService
    {
        event Action<float> Jumped;
        void Charge();
    }
}