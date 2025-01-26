using TMPro;
using UnityEngine;
using UnityMVVM;

namespace BubbleJump.UI.Score
{
    public class ScoreView : ViewBehaviour<IScoreViewModel>
    {

        [SerializeField]
        private TMP_Text _score;

        [SerializeField]
        private TMP_Text _record;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SmartBind(ViewModel!.Score, x => _score.text = x.ToString("0") + " meters");
            SmartBind(ViewModel!.Record, x => _record.text = "record: " + x.ToString("0"));
        }
    }
}