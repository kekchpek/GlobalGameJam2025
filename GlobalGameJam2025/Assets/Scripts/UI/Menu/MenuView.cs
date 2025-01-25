using System;
using TMPro;
using UnityEngine;
using UnityMVVM;

namespace BubbleJump.UI.Menu
{
    public class MenuView : ViewBehaviour<IMenuViewModel>
    {

        [SerializeField]
        private TMP_Text _recordText;

        [SerializeField]
        private GameObject _startLayout;

        [SerializeField]
        private GameObject _loseLayout;

        [SerializeField]
        private GameObject _bg;
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SmartBind(ViewModel!.LoseRecord, x => _recordText.text = x.ToString("0") + "meters");
            SmartBind(ViewModel.StartLayoutShown, UpdateLayout);
            SmartBind(ViewModel.LoseLayoutShown, UpdateLayout);
        }

        private void UpdateLayout()
        {
            _startLayout.SetActive(ViewModel!.StartLayoutShown.Value);
            _loseLayout.SetActive(ViewModel!.LoseLayoutShown.Value);
            _bg.SetActive(ViewModel!.StartLayoutShown.Value || ViewModel!.LoseLayoutShown.Value);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ViewModel?.OnSpaceClicked();
            }
        }
    }
}