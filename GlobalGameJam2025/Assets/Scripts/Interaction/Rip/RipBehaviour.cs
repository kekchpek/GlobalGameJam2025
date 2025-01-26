using TMPro;
using UnityEngine;

namespace BubbleJump.Interaction.Rip
{
    public class RipBehaviour : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public void SetTitle(string title)
        {
            _text.text = title;
        }
    }
}