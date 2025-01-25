using UnityEngine;
using UnityEngine.UI;
using UnityMVVM;

namespace BubbleJump.UI.SuperJump
{
    public class SuperJumpView : ViewBehaviour<ISuperJumpViewModel>
    {
        
        [SerializeField]
        private Image _chargeIndicator;

        [SerializeField]
        private GameObject _tutorLayout;

        [SerializeField]
        private GameObject _chargeLayout;
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SmartBind(ViewModel!.IndicatorPercent, x => _chargeIndicator.fillAmount = x);
            SmartBind(ViewModel!.TutorialShown, _tutorLayout.SetActive);
            SmartBind(ViewModel!.IndicatorShown, _chargeLayout.SetActive);
        }
        
        
    }
}