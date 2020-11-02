using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphereenemy : MonoBehaviour
{
    Vector3 rot = Vector3.zero;
    float rotSpeed = 40f;
    [SerializeField]
    private float _walkSpeed = 1.0f;
    Animator anim;
    float nowTime;
	bool isRedy = false;

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
		if (anim.GetBool("Roll_Anim") == false)
		{
			nowTime += Time.deltaTime;
		}
		if (nowTime > 3.0f)
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
		if (other.tag == "Player")
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
			isRedy = anim.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("cloed_Roll_Loop"));
			Debug.Log(isRedy);
			if (isRedy)
			{
				transform.root.position += transform.forward * Time.deltaTime * _walkSpeed * 2;
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
}
