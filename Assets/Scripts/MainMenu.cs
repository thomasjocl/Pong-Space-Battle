using Assets.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayP1vsCPU()
    {
        SceneParameters.GameType = GameType.vsCpu;
        SceneManager.LoadScene("Scenes/GameDefaultScene");
    }

    public void PlayP1vsP2()
    {
        SceneParameters.GameType = GameType.vsP2;
        SceneManager.LoadScene("Scenes/GameDefaultScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
