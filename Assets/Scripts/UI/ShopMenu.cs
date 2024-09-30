using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using TowerDefence.Manager;

namespace TowerDefence.UI
{
    public class ShopMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;

        private Animator _anim;
        private bool _isOpen = false;

        void OnGUI()
        {
            currencyText.text = LevelManager.Instance.currency.ToString();
        }

        void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        public void ToggleShop()
        {
            _isOpen = !_isOpen;
            _anim.SetBool("isOpen", _isOpen);
        }

        public void Menu()
        {
            SceneManager.LoadScene(0);
        }
    }
}