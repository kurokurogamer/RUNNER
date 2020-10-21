using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Tooltip("ターゲット")]
    private Transform _target = null;
    [SerializeField, Tooltip("カメラの座標")]
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Rotate()
    {
        // 右回転
        if (Input.GetKey(KeyCode.Z))
        {
            transform.RotateAround(_target.position, Vector3.up, 1f);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            transform.RotateAround(_target.position, Vector3.up, -1f);
        }
        if (Input.GetKey(KeyCode.C))
        {
            transform.RotateAround(_target.position, transform.right, -1f);
        }
        else if (Input.GetKey(KeyCode.V))
        {
            transform.RotateAround(_target.position, transform.right, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

	private void LateUpdate()
	{
		Rotate();
	}
}
