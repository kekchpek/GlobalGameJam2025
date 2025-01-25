using System.Collections.Generic;
using System.Linq;
using BubbleJump.Infrastructure.Assets;
using BubbleJump.Infrastructure.Mvvm;
using BubbleJump.Infrastructure.Time;
using BubbleJump.Model.Player;
using BubbleJump.Model.Startup;
using BubbleJump.Model.SuperJump;
using BubbleJump.UI;
using BubbleJump.UI.SuperJump;
using UnityEngine;
using UnityMVVM;
using UnityMVVM.DI;
using UnityMVVM.DI.Config;
using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.PrefabsProvider;
using Zenject;

namespace BubbleJump.Configuration
{
    public class CoreInstaller : MonoInstaller
    {

        [SerializeField]
        private List<Transform> _uiLayers = new();
        
        public override void InstallBindings()
        {
            Container.UseAsMvvmContainer(_uiLayers.Select(x => (x.name, x)).ToArray(),
                new MvvmContainerConfiguration
                {
                    
                });
            Container.FastBind<IViewsPrefabsProvider, ViewsPrefabsProvider>();
            Container.FastBindMono<ITimeManager, TimeManager>();
            Container.FastBind<IAssetsModel, AddressablesAssetsModel>();
            
            Container.FastBind<IPlayerModel, PlayerModel>();
            
            Container.Bind<IStartupService>().To<StartupService>().AsSingle();
            Container.FastBind<ISuperJumpService, SuperJumpService>();
            Container.FastBind<ISuperJumpMutableModel, ISuperJumpModel, SuperJumpModel>();
            
            Container.InstallView<SuperJumpView, ISuperJumpViewModel, SuperJumpViewModel>();
            Container.InstallView<EmptyView, IViewModel, ViewModel>(ViewNames.GameScreen);
        }
    }
}