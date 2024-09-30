using TowerDefence.Turret;
using UnityEngine;

namespace TowerDefence.Manager
{
    public class BuildTowerManager : MonoBehaviour
    {
        public static BuildTowerManager instance;

        [SerializeField] private Tower[] towers;

        private int _selectedTower = 0;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public Tower GetSelectedTower()
        {
            return towers[_selectedTower];
        }

        public void SetSelectedTower(int selectedTower)
        {
            _selectedTower = selectedTower;
        }
    }
}