using TowerDefence.Turret;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence.Manager
{
    public class GridManager : MonoBehaviour
    {
        public TurretController turret;
        [SerializeField] private Color selectedGridColor;
        private GameObject _tower;
        private SpriteRenderer _grid;
        private Color _startColor = Color.white;

        private void Awake()
        {
            _grid = GetComponent<SpriteRenderer>();
        }

        void OnMouseEnter()
        {
            _grid.color = selectedGridColor;
        }

        void OnMouseExit()
        {
            _grid.color = _startColor;
        }

        private void OnMouseDown()
        {
            if (_tower != null)
            {
                turret.OpenUpgradeUI();
                return;
            }
            if (IsMouseOverUI()) return;
            Tower _towerToBuild = BuildTowerManager.instance.GetSelectedTower();

            if (_towerToBuild.cost > LevelManager.Instance.currency)
            {
                Debug.Log("Not Enough Currency!");
                return;
            }

            LevelManager.Instance.SpendCurrency(_towerToBuild.cost);

            _tower = Instantiate(_towerToBuild.prefab, transform.position, Quaternion.identity);
            turret = _tower.GetComponent<TurretController>();
        }

        private bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}