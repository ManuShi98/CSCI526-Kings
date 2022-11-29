using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void BackToGame()
    {
        EventBus.post<GameStartEvent>(new GameStartEvent() { });
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        WeatherSystem.ResetWeather();
        SeasonController.ResetSeason();
    }
}
