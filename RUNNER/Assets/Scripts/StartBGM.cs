using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGM : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instans.PlayBGM(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
