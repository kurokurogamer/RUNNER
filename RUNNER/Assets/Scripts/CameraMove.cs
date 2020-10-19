using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Tooltip("ターゲット")]
    private Transform _target = null;
    // 初期座標
    private Vector3 _firstPos;

    // Start is called before the first frame update
    void Start()
    {
        _firstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

	private void LateUpdate()
	{
        transform.position = _firstPos + _target.position;
	}
}
