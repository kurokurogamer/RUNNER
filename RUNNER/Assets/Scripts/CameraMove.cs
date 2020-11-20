using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Tooltip("ターゲット")]
    private Transform _target = null;
    [SerializeField, Tooltip("カメラの座標")]
    private Vector3 _offset = Vector3.zero;
    [SerializeField]
    private float _speed = 1.0f;
    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        rot = transform.eulerAngles;
    }

    private void Follow()
    {
        transform.position = _target.transform.position - transform.forward * _offset.z + transform.up * _offset.y;
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
        rot = Vector3.Lerp(rot, _target.transform.eulerAngles, Time.deltaTime * _speed);

		transform.rotation = Quaternion.Euler(_target.transform.eulerAngles);
	}

    // Update is called once per frame
    void Update()
    {

    }

	private void LateUpdate()
	{
  //      Follow();
		//Rotate();
	}

	private void FixedUpdate()
	{
        Follow();
		Rotate();
	}
}
