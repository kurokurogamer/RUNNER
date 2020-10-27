using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    bool IsFadeOut; //フェードアウトする
    public bool IsFade; //フェードの演出
    public float time; //フェードにかかる時間
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFade)
        {
            if (IsFadeOut)
            {
                //フェードアウトの処理
                if (image.color.a < 1)
                {
                    if (image.color.a < 1)
                    {
                        if (image.color.a < 1)
                   
                            image.color
                                = new Color(0, 0, 0, image.color.a + 1 / (60 * time));
                        else image.color = new Color(0, 0, 0, 1);
             
                    }
                }
                else
                {
                    //フェードインの処理
                    if (image.color.a >= 0) 

                        image.color 
                            = new Color(0, 0, 0, image.color.a - 1 / (60 * time));
                    else image.color = new Color(0, 0, 0, 0);
                }
            }
        }
    }

    public void StartFade(float t, bool fadeout)
    {
        time = t;
        IsFadeOut = fadeout ? true : false;
        if (IsFadeOut) image.color = new Color(0, 0, 0, 0);
        else image.color = new Color(0, 0, 0, 1);
        IsFade = true;
    }
}
