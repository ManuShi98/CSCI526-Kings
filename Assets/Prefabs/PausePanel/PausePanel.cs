using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToGame()
    {
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
    }
}
