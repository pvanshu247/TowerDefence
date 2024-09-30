using TowerDefence.Enemy;
using UnityEngine;

namespace TowerDefence.Turret
{
    public class HomingBullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed = 5f;
        private int _bulletDamage;
        private Transform _target;
        private Rigidbody2D _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetDamage(int damage)
        {
            _bulletDamage = damage;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        void FixedUpdate()
        {
            if (!_target) return;
            Vector2 direction = (_target.position - transform.position).normalized;
            _rb.velocity = direction * bulletSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.transform.GetComponent<EnemyHealth>()?.TakeDamage(_bulletDamage);
            Destroy(gameObject);
        }
    }
}