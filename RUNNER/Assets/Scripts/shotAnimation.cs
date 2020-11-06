using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotAnimation : MonoBehaviour
{
    private Animator animator;
    private float _nowTime = 0.0f;
    [SerializeField]
    private GameObject _bullet = null;
    [SerializeField]
    private GameObject ShotPoint;
  
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _nowTime += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.LookAt(other.transform);
            if(_bullet != null && ShotPoint != null &&_nowTime >= 3.0f)
            {
                animator.SetBool("shot", true);
                _nowTime = 0.0f;
                Instantiate(_bullet, ShotPoint.transform.position, transform.rotation);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {     
        if (other.CompareTag("Player"))
        {
            animator.SetBool("shot",false);
        }
    }
}
