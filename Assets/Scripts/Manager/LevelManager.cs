using UnityEngine;

namespace TowerDefence.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }
        public Transform startPoint;
        public Transform[] wayPoints;
        public int currency;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void IncreaseCurrency(int amount)
        {
            currency += amount;
        }

        public bool SpendCurrency(int amount)
        {
            if (amount <= currency)
            {
                currency -= amount;
                return true;
            }
            else
            {
                Debug.Log("Not enough currency");
                return false;
            }
        }
    }
}