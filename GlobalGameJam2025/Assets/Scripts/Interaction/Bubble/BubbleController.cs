using UnityEngine;

namespace BubbleJump
{
    public class BubbleController : MonoBehaviour
    {
        private int _hp;
        private Animator _animator;

        void Start()
        {
            _hp = Random.Range(3, 7);
            //_animator = GetComponent<Animator>();
        }

        void Update()
        {
            if(_hp <= 0)
            {
                //_animator.SetBool("Explode",true);
                Destroy(this, 0.5f);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _hp--;
                //_animator.SetTrigger("Shake");
            }
        }
    }
}
