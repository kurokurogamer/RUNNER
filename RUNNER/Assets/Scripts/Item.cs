using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private PointCount _pointCount;
    [SerializeField]
    private int _point = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 15, 0) * Time.deltaTime);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        //Objectを取得
        if (other.tag == "Player")
        {
            _pointCount.count += _point;
            Debug.Log("アイテムの取得");
            //Objectを消す
            Destroy(gameObject);
        }
    }
}
