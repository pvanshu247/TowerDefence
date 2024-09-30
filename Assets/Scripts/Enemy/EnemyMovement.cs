using System;
using TowerDefence.Manager;
using UnityEngine;

namespace TowerDefence.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public static Action OnEnemyKilled;
        [SerializeField] private float speed = 4f;
        private Rigidbody2D _rb;
        private Transform _target;
        private int _pathIndex = 0;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            _target = LevelManager.Instance.wayPoints[_pathIndex];
        }

        void Update()
        {
            EnemyNavigation();
        }

        private void EnemyNavigation()
        {
            if (Vector2.Distance(_target.position, transform.position) <= 0.1f)
            {
                _pathIndex++;
                if (_pathIndex == LevelManager.Instance.wayPoints.Length)
                {
                    OnEnemyKilled?.Invoke();
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    _target = LevelManager.Instance.wayPoints[_pathIndex];
                }
            }
        }

        void FixedUpdate()
        {
            EnemyMovementAndRotation();
        }

        private void EnemyMovementAndRotation()
        {
            Vector2 _direction = (_target.position - transform.position).normalized;
            _rb.velocity = _direction * speed;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}