using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private string startLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
