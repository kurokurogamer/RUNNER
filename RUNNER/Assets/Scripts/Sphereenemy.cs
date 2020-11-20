using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEnemy : MonoBehaviour
{
	private Rigidbody _rigid;
    private Animator _anim;

    [SerializeField, Tooltip("歩行速度")]
    private float _walkSpeed = 1.0f;
	private Vector3 _rot;
    private float _nowTime;
	private bool _isReady;

	// Use this for initialization
	void Awake()
    {
		_rigid = GetComponent<Rigidbody>();
		foreach(Transform child in transform)
		{
			_anim = child.GetComponent<Animator>();
		}

		transform.eulerAngles = _rot;
    }


	private void Start()
	{
		_nowTime = 0;
		_isReady = false;
	}

	private void Walk()
	{
		if (_anim.GetBool("Roll_Anim") == false)
		{
			_nowTime += Time.deltaTime;
		}
		if (_nowTime > 3.0f)
		{
			_anim.SetBool("Walk_Anim", true);
			bool ready = _anim.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("anim_Walk_Loop"));
			if (ready)
			{
				_rigid.velocity = transform.forward * _walkSpeed;
			}
		}
	}


	// Update is called once per frame
	void Update()
	{
		Walk();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_nowTime = 0;
			_anim.SetBool("Walk_Anim", false);
			_anim.SetBool("Roll_Anim", true);
			// 敵が範囲内に入ったので加速度をリセット
			_rigid.velocity = Vector3.zero;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			transform.LookAt(other.transform);
			if (_anim.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("cloed_Roll_Loop")))
			{
				// 自身の正面方向を得る
				Vector3 forward = transform.forward;
				// 空中を移動できるわけではないのでY軸の移動量を消す
				forward = new Vector3(forward.x, 0, forward.z);
				// 得られたベクトルを単位ベクトルにすることで2次元の移動量を算出
				_rigid.velocity = forward.normalized * _walkSpeed * 2;
			}
		}

		transform.position += transform.right * 1 * 10 + transform.forward * 1 * 10;

		RaycastHit hit;
		var isHit = Physics.Raycast(transform.position, -transform.up, out hit, 100, 1, QueryTriggerInteraction.Ignore);

		if (isHit)
		{
			Debug.Log("回転");
			Vector3 onPlane = Vector3.ProjectOnPlane(Vector3.up, hit.normal);
			transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
			transform.position = hit.point + transform.up;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			_anim.SetBool("Roll_Anim", false);
		}

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_anim.SetBool("Roll_Anim", false);
		}
	}
}
