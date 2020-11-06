using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 velocity;
    [SerializeField]
    private float RunSpeed;
    private Animator animator;
    [SerializeField]
    private int _hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        UnityChanAnimation();
    }

    bool InputCheck()
    {
        bool isret = false;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * Time.deltaTime * 3.0f;
            isret = true;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.forward * Time.deltaTime * 3.0f;
            isret = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * Time.deltaTime * 3.0f;
            animator.SetFloat("Curve", 1.0f);
            isret = true;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * Time.deltaTime * 3.0f;
            animator.SetFloat("Curve", -1.0f);
            isret = true;
        }
        else
        {
            animator.SetFloat("Curve", 0);
        }

        return isret;
    }

    void UnityChanAnimation()
    {
        animator.SetFloat("Speed", 0.0f);
        animator.SetBool("Brake", false);
        if (InputCheck())
        {
            if (Input.GetKey(KeyCode.Z))
            {
                animator.SetFloat("Speed", 0.6f);
            }
            else
            {
                animator.SetFloat("Speed", 0.2f);
            }
            if(Input.GetKey(KeyCode.X))
            {
                animator.SetBool("Brake", true);
            }
        }

        var clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
        if(clipInfo.clip.name == "DAMAGED01")
        {
            var info = animator.GetCurrentAnimatorStateInfo(0);
            if (_hp <= 0)
            {
                if (info.normalizedTime >= 0.5f)
                {
                    animator.speed = 0;
                }
            }
            else
            {
                if (info.normalizedTime >= 1.0f)
                {
                    animator.Play("Wait");
                }
            }

        }
        Debug.Log(animator.GetFloat("Speed"));
    }
    public void Damege()
    {
        animator.Play("Deth");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Damege();
            bool isReady = animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Deth"));
            if (!isReady)
            {
                _hp--;
            }
        }
    }
}
