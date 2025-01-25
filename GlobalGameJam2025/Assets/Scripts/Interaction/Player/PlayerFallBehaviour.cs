using System;
using BubbleJump.Model.Player;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    public class PlayerFallBehaviour : MonoBehaviour
    {

        private IPlayerService _playerService;
        private IPlayerModel _playerModel;

        [Inject]
        public void Construct(
            IPlayerModel playerModel,
            IPlayerService playerService)
        {
            _playerService = playerService;
            _playerModel = playerModel;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Floor"))
            {
                if (!_playerModel.IsOnTheGround.Value)
                {
                    _playerService.Die();
                    Destroy(this);
                }
            }
        }
    }
}