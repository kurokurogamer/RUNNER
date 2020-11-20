using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private int _hp = 3;

    private Vector2 _Axis;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _Axis = Vector2.zero;
    }


    // Update is called once per frame
    void Update()
    {
        UnityChanAnimation();
    }

    bool InputCheck()
    {
        _Axis = Vector2.zero;
        _Axis.x = Input.GetAxis("Horizontal");
        _Axis.y = Input.GetAxis("Vertical");

        // 左右判定
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _Axis.x = -1;
            animator.SetFloat("Curve", 1.0f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _Axis.x = -1;
            animator.SetFloat("Curve", -1.0f);
        }
        else
		{
            animator.SetFloat("Curve", 0);
        }

        // 上下判定
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _Axis.y = -1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _Axis.y = 1;
        }

        if(_Axis.x != 0 || _Axis.y != 0)
		{
            return true;
		}
        else
		{
            return false;
		}
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
