using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAnimation : MonoBehaviour
{
    private Animator animator;
    private float _nowTime = 0.0f;
    [SerializeField]
    private GameObject _bullet = null;
    [SerializeField]
    private GameObject ShotPoint;
    private EnemySearch _search;
    private GameObject _player;
  
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _search = transform.parent.GetComponent<EnemySearch>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_search._sati)
		{
            if (_search._target != null)
            {
                transform.LookAt(_search._target.transform);
                if (_bullet != null && ShotPoint != null && _nowTime >= 0.5f)
                {
                    animator.SetBool("shot", true);
                    _nowTime = 0.0f;
                    GameObject obj = Instantiate(_bullet, ShotPoint.transform.position, transform.rotation);
                    obj.transform.LookAt(_search._target.transform);

                }
            }
        }
        else
		{
            animator.SetBool("shot", false);
        }
        _nowTime += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }

    void OnTriggerExit(Collider other)
    {     
        if (other.CompareTag("Player"))
        {
            //animator.SetBool("shot",false);
        }
    }
}
