namespace BubbleJump.Model.Player
{
    public interface IPlayerService
    {

        void Die();

        void Enable();
        
        void Respawn();

        void TrackHeight(float height);

    }
}