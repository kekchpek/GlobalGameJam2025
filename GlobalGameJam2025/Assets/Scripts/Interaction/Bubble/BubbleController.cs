using UnityEngine;

namespace BubbleJump
{
    public class BubbleController : MonoBehaviour
    {
        [SerializeField]
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
                Destroy(gameObject, 0.5f);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (collision.collider.CompareTag("Player"))
            {
                _hp--;
                //_animator.SetTrigger("Shake");
            }
        }
    }
}
