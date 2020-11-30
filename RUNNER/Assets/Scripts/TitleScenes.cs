using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScenes : MonoBehaviour
{
    [SerializeField] 
    private AudioClip clip;

    private void Update()
    {
        OnClickStartButton();
    }

    private void OnClickStartButton()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            AudioManager.instans.PlayOneSE(clip);

            SceneCtl.instans.LoadScene("Select");
        }
    }
}