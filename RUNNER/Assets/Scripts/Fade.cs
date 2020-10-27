using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField]
    private bool _loop = true;
    [SerializeField]
    private float _fadeTime = 1;
    private float _nowTime;
    private float _alpha;

    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _nowTime = 0;
        _alpha = 0;
        _image = GetComponent<Image>();
    }

    private void FadeOut()
	{
        //_nowTime += Time.deltaTime;
        _alpha += Mathf.MoveTowards(_alpha, 1.0f, _fadeTime);
        if(_alpha >= 1.0f)
		{
            //_nowTime = 0;
            _alpha = 1.0f;
		}
	}

    private void FadeIn()
	{
        _alpha += Mathf.MoveTowards(_alpha, 0.0f, _fadeTime);
        if (_alpha >= 1.0f)
        {
            //_nowTime = 0;
            _alpha = 0.0f;
        }
    }

    private void FadeLoop()
	{

	}

    private void SetColor()
	{
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _alpha);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
