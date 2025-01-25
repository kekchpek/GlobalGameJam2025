using UnityEngine;

namespace BubbleJump.Interaction.Bubble
{
    public interface IGravityTarget
    {
        Transform Transform { get; }
        bool IsEnabled { get; }
    }
}