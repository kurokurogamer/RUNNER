using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtl : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーの歩行速度")]
    private float _speed = 1.0f;
    private const float MAX_SPEED = 10.0f;
    [SerializeField, Tooltip("RayCast判定用レイヤーマスク")]
    private LayerMask _layerMask = 0;
    [SerializeField,Tooltip("Rayの飛距離")]
    private float _distance = 1.0f;
    private RadialBlur _blur;
    private Coroutine _coroutine = null;

    // 入力情報
    private Vector2 _Axis;
    private Vector3 vec = Vector3.zero;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.GetComponent<Camera>();
        _blur = Camera.main.GetComponent<RadialBlur>();
    }


    private void MoveCheck()
	{
        _Axis = Vector2.zero;
        _Axis.x = Input.GetAxis("Horizontal");
        _Axis.y = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.Alpha4))
		{
            _Axis.x = -1;
		}
        else if(Input.GetKey(KeyCode.Alpha6))
		{
            _Axis.x = -1;
		}
        if(Input.GetKey(KeyCode.Alpha8))
		{
            _Axis.y = -1;
        }
        else if(Input.GetKey(KeyCode.Alpha2))
		{
            _Axis.y = 1;
		}
        Debug.Log((transform.right * _Axis.x));
        if (_Axis.x != 0 || _Axis.y != 0)
        {
            _blur.Strength = Mathf.MoveTowards(_blur.Strength, 0.2f, Time.deltaTime);
            transform.position += transform.right * _Axis.x *_speed + transform.forward * _Axis.y * _speed;
            //transform.position += transform.right * _Axis.x * _speed + transform.forward * _Axis.y * _speed;
        }
        else
        {
            _blur.Strength = Mathf.MoveTowards(_blur.Strength, 0, Time.deltaTime);
        }
    }

    private void Rotate()
	{
        RaycastHit hit;
        //transform.rotation = Quaternion.Euler(new Vector3(0, _camera.transform.eulerAngles.y, 0));
        var isHit = Physics.Raycast(transform.position, -transform.up, out hit, _distance, _layerMask, QueryTriggerInteraction.Ignore);

        if (isHit)
        {
            Debug.Log("回転");
            Vector3 onPlane = Vector3.ProjectOnPlane(Vector3.up, hit.normal);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.position = hit.point + transform.up;
            vec = onPlane;
            Debug.Log(hit.normal);
            Debug.Log(onPlane);
			//transform.rotation = Quaternion.LookRotation(onPlane);
		}
    }

    // Update is called once per frame
    void Update()
    {
        MoveCheck();
        Rotate();
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        RaycastHit hitF;
        var isHit = Physics.Raycast(transform.position, -transform.up, out hit, _distance, _layerMask, QueryTriggerInteraction.Ignore);
        var isHitF = Physics.Raycast(transform.position, transform.forward, out hitF, _distance, _layerMask, QueryTriggerInteraction.Ignore);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + -transform.up * hit.distance);
            Debug.Log(hit.normal);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + -transform.up * _distance);
        }

        if (isHitF)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * hitF.distance);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * _distance);
        }
        Vector3 onPlane = Vector3.ProjectOnPlane(transform.forward * _speed, hit.normal);
        vec = onPlane;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (transform.position + 5 * vec));

    }

}
