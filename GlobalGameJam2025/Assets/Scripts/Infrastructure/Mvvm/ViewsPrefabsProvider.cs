using BubbleJump.Infrastructure.Assets;
using UnityEngine;
using UnityMVVM.ViewModelCore.PrefabsProvider;

namespace BubbleJump.Infrastructure.Mvvm
{
    public class ViewsPrefabsProvider : IViewsPrefabsProvider
    {
        private readonly IAssetsModel _assetsModel;

        public ViewsPrefabsProvider(IAssetsModel assetsModel)
        {
            _assetsModel = assetsModel;
        }
        
        public GameObject GetViewPrefab(string viewName)
        {
            return _assetsModel.GetCachedAsset<GameObject>(viewName);
        }
    }
}