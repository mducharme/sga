using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private string startLevel;
    [SerializeField] private Game.SaveManager saveManager;

    [SerializeField] private GameObject noSavedGamePanel;
    [SerializeField] private GameObject withSavedGamePanel;

    [SerializeField] private GameObject noSavedGameFirstButton;
    [SerializeField] private GameObject withSavedGameFirstButton;

    public void Start()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        if (saveManager.GetSavedGames().Count < 1)
        {
            // No saved games
            noSavedGamePanel.SetActive(true);
            withSavedGamePanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(noSavedGameFirstButton);
        }
        else
        {
            // Save games available
            noSavedGamePanel.SetActive(false);
            withSavedGamePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(withSavedGameFirstButton);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startLevel);
        Player.PlayerController.instance.gameObject.SetActive(true);
        Player.PlayerController.instance.transform.position = new Vector3(0, 1, 0);
    }

    public void ContinueGame()
    {
        // This will load scene
        saveManager.LoadLatest();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
        