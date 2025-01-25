using UnityEngine;

namespace BubbleJump.Model.Player
{
    public interface IPlayerMutableModel : IPlayerModel
    {
        void SetPlayerOnTheGround(bool isPlayerOnTheGround);
        void SetDead(bool isDead);
        void SetTransform(Transform transform);
        void SetPlayerHeight(float height);
        void SetPlayerRecord(float record);
        void SetEnabled(bool isEnabled);
    }
}