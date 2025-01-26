using System;
using LaughGame.WebNative;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityMVVM;
using UnityMVVM.ViewModelCore;

namespace BubbleJump.UI.MobileWarning
{
    public class MobileWarningView : ViewBehaviour<IViewModel>
    {

        private const string Link = "https://kekchpek.itch.io/ggj2025";

        [SerializeField]
        private Button _copyButton;

        [SerializeField]
        private GameObject _layout;

        [SerializeField]
        private GameObject _copiedLayout;

        [SerializeField]
        private GameObject _notCopiedLayout;

        private void Awake()
        {
            _copyButton.onClick.AddListener(() =>
            {
                GUIUtility.systemCopyBuffer = Link;
                LaughGameWeb.CopyToClipboard();
            });
        }

        private void Update()
        {
            _layout.SetActive(LaughGameWeb.IsMobile());
            _copiedLayout.SetActive(Link == GUIUtility.systemCopyBuffer);
            _notCopiedLayout.SetActive(Link != GUIUtility.systemCopyBuffer);
        }
    }
}