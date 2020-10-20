using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RadialBlur : MonoBehaviour
{

    [SerializeField,Tooltip("Hidden/RadialBlurを指定")]
    private Shader _shader = null;
    [SerializeField, Range(4, 16), Tooltip("歪む枚数")]
    private int _sampleCount = 8;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("歪む強さ")]
    private float _strength = 0.5f;

    public float Strength
	{
		get { return _strength; }
        set { _strength = value; }
	}

    private Material _material = null;

    private void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        if (_material == null)
        {
            if (_shader == null)
            {
                Graphics.Blit(source, dest);
                return;
            }
            else
            {
                _material = new Material(_shader);
            }
        }
        _material.SetInt("_SampleCount", _sampleCount);
        _material.SetFloat("_Strength", _strength);
        Graphics.Blit(source, dest, _material);
    }
}
