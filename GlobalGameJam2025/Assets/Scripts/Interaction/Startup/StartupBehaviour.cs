using System;
using BubbleJump.Model.Startup;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Startup
{
    public class StartupBehaviour : MonoBehaviour
    {

        private IStartupService _startupService;

        [Inject]
        public void Inject(IStartupService startupService)
        {
            _startupService = startupService;
        }

        private void Awake()
        {
            _startupService.Startup();
        }
    }
}