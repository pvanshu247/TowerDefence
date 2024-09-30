using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence.UI
{
    public class UpgradeTurretUI : MonoBehaviour, IPointerExitHandler
    {
        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }
    }
}