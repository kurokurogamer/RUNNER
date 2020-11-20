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
    private Rigidbody _rigid;
    private Animator _animator;

    [SerializeField]
    private int _hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.GetComponent<Camera>();
        _blur = Camera.main.GetComponent<RadialBlur>();
        _rigid = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void MoveCheck()
	{
        _Axis = Vector2.zero;
        _Axis.x = Input.GetAxis("Horizontal");
        _Axis.y = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Alpha4))
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
        if (_Axis.x != 0 || _Axis.y != 0)
        {
           if (Input.GetKey(KeyCode.Z))
            {
                _rigid.velocity = transform.right * _Axis.x * _speed * 7 + transform.forward * _Axis.y * _speed * 7;
                _animator.speed = 2;
                _blur.Strength = Mathf.MoveTowards(_blur.Strength, 0.2f, Time.deltaTime);
            }
            else
            {
                _rigid.velocity = transform.right * _Axis.x * _speed + transform.forward * _Axis.y * _speed;
                _animator.speed = 1;
                _blur.Strength = Mathf.MoveTowards(_blur.Strength, 0.0f, Time.deltaTime);
            }
        }
        else
        {
			_rigid.velocity -= transform.up;
			_blur.Strength = Mathf.MoveTowards(_blur.Strength, 0, Time.deltaTime);
        }
    }

    private void Rotate()
	{
		if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, _distance, _layerMask, QueryTriggerInteraction.Ignore))
		{
			//Vector3 onPlane = Vector3.ProjectOnPlane(Vector3.up, hit.normal);
			//transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
			//transform.rotation = Quaternion.LookRotation(onPlane);
            //transform.position = hit.point + transform.up;
		}
	}

    private void FlontRayCheck()
	{
        RaycastHit hit;
        var isHit = Physics.Raycast(transform.position + transform.forward, -transform.up, out hit, _distance, _layerMask, QueryTriggerInteraction.Ignore);
    }

    // Update is called once per frame
    void Update()
    {
        // 回転処理
        Rotate();
        // 歩行処理
        MoveCheck();

        var clipInfo = _animator.GetCurrentAnimatorClipInfo(0)[0];
        if (clipInfo.clip.name == "DAMAGED01")
        {
            var info = _animator.GetCurrentAnimatorStateInfo(0);
            if (_hp <= 0)
            {
                if (info.normalizedTime >= 0.5f)
                {
                    _animator.speed = 0;
                }
            }
            else
            {
                if (info.normalizedTime >= 1.0f)
                {
                    _animator.Play("Wait");
                }
            }
        }

        //_rigid.velocity -= transform.up;
    }

    private void OnDrawGizmos()
    {
       var isHit = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, _distance, _layerMask, QueryTriggerInteraction.Ignore);
        var isHitF = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitF, _distance * 5, _layerMask, QueryTriggerInteraction.Ignore);
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + -transform.up * hit.distance);
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
            Gizmos.DrawLine(hitF.point, hitF.point + hitF.normal * hitF.distance);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * _distance);
        }

        Vector3 onPlane = Vector3.ProjectOnPlane(transform.forward * _speed, hit.normal);
        vec = onPlane;

        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position, (transform.position + 5 * vec));

        var transF = Physics.Raycast(transform.position + transform.forward, -transform.up, out hit, _distance, _layerMask, QueryTriggerInteraction.Ignore);
        if(transF)
		{
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + transform.forward, transform.position + transform.forward + -transform.up * hit.distance);
        }
        else
		{
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position + transform.forward, transform.position + transform.forward + -transform.up * _distance);
        }
    }

	private void OnCollisionEnter(Collision collision)
	{
        // 正面に対するRay
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _distance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log(Vector3.Distance(transform.position, hit.point) + "m 離れている");
            //Vector3 onPlane = Vector3.ProjectOnPlane(transform.forward, hit.normal);
            //transform.rotation = Quaternion.LookRotation(onPlane, hit.normal);
        }
    }
}
