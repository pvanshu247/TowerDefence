using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject instructionsPanel;
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void Instructions()
        {
            instructionsPanel.gameObject.SetActive(true);
        }

        public void Back()
        {
            instructionsPanel.gameObject.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}