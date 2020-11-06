using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFreeAnim : MonoBehaviour {

	Vector3 rot = Vector3.zero;
	float rotSpeed = 40f;
	[SerializeField]
	private float _walkSpeed = 1.0f;
	Animator anim;
	float nowTime;

	// Use this for initialization
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		gameObject.transform.eulerAngles = rot;
	}

	private void Start()
	{
		nowTime = 0;
	}
	// Update is called once per frame
	void Update()
	{
		if(anim.GetBool("Roll_Anim") == false)
		{
			nowTime += Time.deltaTime;
		}
		if(nowTime > 3.0f)
		{
			anim.SetBool("Walk_Anim", true);
			Debug.Log("test");
			transform.root.position += transform.forward * Time.deltaTime * _walkSpeed;
		}
		//CheckKey();
		gameObject.transform.eulerAngles = rot;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			nowTime = 0;
			anim.SetBool("Walk_Anim", false);
			anim.SetBool("Roll_Anim", true);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			transform.LookAt(other.transform);
		}	
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			anim.SetBool("Roll_Anim", false);
		}
	}

	void CheckKey()
	{
		// Walk
		if (Input.GetKey(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", true);
		}
		else if (Input.GetKeyUp(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Left
		if (Input.GetKey(KeyCode.A))
		{
			rot.y -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.D))
		{
			rot.y += rotSpeed * Time.fixedDeltaTime;
		}

		// Roll
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (anim.GetBool("Roll_Anim"))
			{
				anim.SetBool("Roll_Anim", false);
			}
			else
			{
				anim.SetBool("Roll_Anim", true);
			}
		}

		// Close
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
			}
		}
	}
}
