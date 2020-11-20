using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(45, 15, 45) * Time.deltaTime);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        //Objectを取得
        if (other.tag == "Player")
        {
            Debug.Log("アイテムの取得");
            //Objectを消す
            Destroy(gameObject);
        }
    }
}
