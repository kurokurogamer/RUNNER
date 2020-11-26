using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphereenemy : MonoBehaviour
{
	private Rigidbody _rigid;
    private Animator anim;

    float rotSpeed = 40f;
    [SerializeField]
    private float _walkSpeed = 1.0f;
	Vector3 rot = Vector3.zero;
    float nowTime;
	bool isReady = false;

	// Use this for initialization
	void Awake()
    {
		_rigid = GetComponent<Rigidbody>();
		foreach(Transform child in transform)
		{
			anim = child.GetComponent<Animator>();
		}

		gameObject.transform.eulerAngles = rot;
    }


	private void Start()
	{
		nowTime = 0;
	}
	// Update is called once per frame
	void Update()
	{
		if (anim.GetBool("Roll_Anim") == false)
		{
			nowTime += Time.deltaTime;
		}
		if (nowTime > 3.0f)
		{
			anim.SetBool("Walk_Anim", true);
			Debug.Log("test");
			bool ready = anim.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("anim_Walk_Loop"));
			if (ready)
			{
				_rigid.velocity = transform.forward * _walkSpeed;
			}

			// transform.position += transform.forward * Time.deltaTime * _walkSpeed;
		}
		//CheckKey();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			nowTime = 0;
			anim.SetBool("Walk_Anim", false);

			anim.SetBool("Roll_Anim", true);
			_rigid.velocity = Vector3.zero;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			transform.LookAt(other.transform);
			isReady = anim.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("cloed_Roll_Loop"));
			Debug.Log(isReady);
			if (isReady)
			{
				Debug.Log(_rigid.velocity);
				_rigid.velocity = transform.forward * _walkSpeed * 2;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			anim.SetBool("Roll_Anim", false);
		}

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			anim.SetBool("Roll_Anim", false);
		}
	}
}
