using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefButton : MonoBehaviour
{
    [SerializeField]
    private Button _button = null;
    // Start is called before the first frame update
    void Start()
    {
        _button.Select();
    }
}
