using UnityEngine;

namespace BubbleJump.Interaction.Bubble
{
    public class GravityTarget : MonoBehaviour, IGravityTarget, IGravityTargetHandle
    {
        public Transform Transform => this ? transform : null;
        public bool IsEnabled { get; private set; } = true;
        public void SetEnabled(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }
    }
}