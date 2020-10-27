using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCtl : MonoBehaviour
{
	private Text timerText;

	public float totalTime;

	public bool Change;

	float seconds;

	int minute;

	float oldSeconds;

	// Use this for initialization
	void Start()
	{
		minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
		timerText = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Change)
		{
			TimeDown();
		}
		else
		{
			TimeUP();
		}

	}

	void TimeDown()
	{

		totalTime -= Time.deltaTime;
		seconds = (int)totalTime;
		timerText.text = seconds.ToString();
	}

	void TimeUP()
	{
		seconds += Time.deltaTime;
		if (seconds >= 60f)
		{
			minute++;
			seconds = seconds - 60;
		}
		//　値が変わった時だけテキストUIを更新
		if ((int)seconds != (int)oldSeconds)
		{
			timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
		}
		oldSeconds = seconds;
	}	
}