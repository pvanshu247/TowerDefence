using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TowerDefence.Manager;

namespace TowerDefence.Turret
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField] private Transform gun;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject upgradeUI;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private int damage;
        [SerializeField] private float targetingRange;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float bulletShotPerSecond = 1f;

        #region Upgrade
        [SerializeField] private int upgradeCost = 100;
        private int _level = 1;
        private float _bulletShotPerSecondUpgrade;
        private float _targetingRangeUpgrade;
        #endregion

        private Transform _target;
        private float _timeSinceLastShot;

        void Start()
        {
            _bulletShotPerSecondUpgrade = bulletShotPerSecond;
            _targetingRangeUpgrade = targetingRange;
            upgradeButton.onClick.AddListener(Upgrade);
        }

        void Update()
        {
            if (_target == null)
            {
                FindTarget();
                return;
            }

            RotateTowardsTarget();

            if (!CheckTargetIsInRange())
            {
                _target = null;
            }
            else
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            _timeSinceLastShot += Time.deltaTime;
            if (_timeSinceLastShot >= 1 / bulletShotPerSecond)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();
                homingBullet.SetDamage(damage);
                homingBullet.SetTarget(_target);
                _timeSinceLastShot = 0f;
            }
        }

        private bool CheckTargetIsInRange()
        {
            return Vector2.Distance(transform.position, _target.position) <= targetingRange;
        }

        private void RotateTowardsTarget()
        {
            float angle = Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            gun.rotation = Quaternion.RotateTowards(gun.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        void FindTarget()
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, targetingRange, transform.position, 0f, enemyMask);
            if (hit.Length > 0)
            {
                _target = hit[0].transform;
            }
        }

        public void OpenUpgradeUI()
        {
            upgradeUI.SetActive(true);
        }

        public void CloseUpgradeUI()
        {
            upgradeUI.SetActive(false);
        }

        public void Upgrade()
        {
            if (CalculateUpgradeCost() > LevelManager.Instance.currency) return;

            LevelManager.Instance.SpendCurrency(CalculateUpgradeCost());
            _level++;
            bulletShotPerSecond = CalculateBulletShotPerSecondUpgrade();
            targetingRange = CalculateUpgradeRange();
            CloseUpgradeUI();
            Debug.Log(CalculateUpgradeCost());
        }

        int CalculateUpgradeCost()
        {
            return Mathf.RoundToInt(upgradeCost * Mathf.Pow(_level, 0.8f));
        }

        float CalculateBulletShotPerSecondUpgrade()
        {
            return _bulletShotPerSecondUpgrade * Mathf.Pow(_level, 0.5f);
        }

        float CalculateUpgradeRange()
        {
            return _targetingRangeUpgrade * Mathf.Pow(_level, 0.5f);
        }

        void OnDrawGizmosSelected()
        {
            Handles.color = Color.magenta;
            Handles.DrawWireDisc(transform.position, Vector3.forward, targetingRange);
        }
    }
}