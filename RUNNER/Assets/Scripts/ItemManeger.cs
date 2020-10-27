using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManeger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");

        //Objectを取得
        if (other.tag == "Player")
        {
            //Objectを消す
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(45, 15, 45) * Time.deltaTime);

       
    }
}
