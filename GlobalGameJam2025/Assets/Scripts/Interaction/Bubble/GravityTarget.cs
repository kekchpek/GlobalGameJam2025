using UnityEngine;

namespace BubbleJump.Interaction.Bubble
{
    public class GravityTarget : MonoBehaviour, IGravityTarget, IGravityTargetHandle
    {
        public Transform Transform => transform;
        public bool IsEnabled { get; private set; }
        public void SetEnabled(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }
    }
}