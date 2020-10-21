using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rigid;

    [SerializeField]
    private float _speed = 1.0f;
    private const float MAX_SPEED = 10.0f;
    [SerializeField]
    private float _distance = 1.0f;
    [SerializeField]
    private LayerMask _layerMask = 0;

    Vector3 vec = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void GroundCheck()
    {
        Vector2 vec = Vector3.zero;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += (transform.right * _speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position -= (transform.right * _speed);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += (transform.forward * _speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= (transform.forward * _speed);
        }

        RaycastHit hit;
        var isHit = Physics.Raycast(transform.position, -transform.up, out hit, _distance, _layerMask, QueryTriggerInteraction.Ignore);

        if (isHit) 
        {
            Debug.Log("回転");
            Vector3 onPlane = Vector3.ProjectOnPlane(transform.forward * _speed, hit.normal);
            Gizmos.DrawLine(transform.position, (transform.position + 5 * onPlane));

            //transform.rotation = Quaternion.LookRotation(transform.forward, onPlane);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
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
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (transform.position + 5 * vec));

    }
}
