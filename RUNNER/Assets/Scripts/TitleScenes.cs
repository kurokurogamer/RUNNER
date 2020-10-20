using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScenes : MonoBehaviour
{

    private void Update()
    {
        OnClickStartButton();
    }

    private void OnClickStartButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("GameScene1");
        }
    }
}