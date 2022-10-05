using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionPanel : MonoBehaviour
{
    public GameObject DialogForQuestion;
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
      DialogForQuestion.SetActive(false);
    }

    public void OnClickForTutorial ()
    {
        DialogForQuestion.SetActive(true);
    }
}
