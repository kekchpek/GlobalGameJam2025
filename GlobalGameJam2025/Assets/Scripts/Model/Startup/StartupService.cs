using BubbleJump.Infrastructure.Assets;
using BubbleJump.UI;
using UnityEngine;
using UnityMVVM.ViewManager;

namespace BubbleJump.Model.Startup
{
    public class StartupService : IStartupService
    {
        
        private readonly IViewManager _viewManager;
        private readonly IAssetsModel _assetsModel;

        public StartupService(
            IViewManager viewManager,
            IAssetsModel assetsModel)
        {
            _viewManager = viewManager;
            _assetsModel = assetsModel;
        }
        
        public async void Startup()
        {
            await _viewManager.Open(LayerNames.Screen, ViewNames.GameScreen);
        }
        
    }
}