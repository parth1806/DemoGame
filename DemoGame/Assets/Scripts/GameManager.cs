using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject levelCompletedPanel;

    // Start is called before the first frame update
    void Start()
    {
        levelCompletedPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelCompleted()
    { 
        Debug.Log("Level Completed");
        levelCompletedPanel.SetActive(true);
    }

    public void OnClick_Restart()
    {
        SceneManager.LoadScene(0);
    }
}
