using TowerDefence.Manager;
using UnityEngine;

namespace TowerDefence.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private int currencyWorth = 20;
        private bool _isDestroyed = false;

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0 && !_isDestroyed)
            {
                EnemyMovement.OnEnemyKilled.Invoke();
                LevelManager.Instance.IncreaseCurrency(currencyWorth);
                _isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }
}