using System.Runtime.InteropServices;
using UnityEngine;

namespace LaughGame.WebNative
{
    public static class LaughGameWeb
    {
        [DllImport("__Internal")]
        private static extern bool LG_IsMobile();
        [DllImport("__Internal")]
        private static extern bool LG_CopyToClipboard();

        public static bool IsMobile() => !Application.isEditor && LG_IsMobile();
        public static bool CopyToClipboard() => LG_CopyToClipboard();
    }
}
