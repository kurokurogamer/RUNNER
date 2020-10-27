using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
    public SceneCtl _instans;

    private Coroutine _coroutin = null;

	private void Awake()
	{
		if(_instans == null)
		{
            _instans = this;
            DontDestroyOnLoad(this.gameObject);
		}
        else
		{
            Destroy(this.gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    public void LoadScene(string name)
	{
        if(name == "")
		{
            return;
		}
        // シーンの切り替え
        SceneManager.LoadScene(name);
	}

    public void AddScene(string name)
	{
        if (name == "")
        {
            return;
        }
        // シーンの追加
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    // 非同期読み込み処理
    public void LoadSceneAsync(string name)
	{
        if (name == "")
        {
            return;
        }
        // コルーチンが回っているか調べる
        if(_coroutin == null)
		{
            _coroutin = StartCoroutine(Load());
		}
    }

    public void UnLoadScene(string name)
	{
        if (name == "")
        {
            return;
        }
        SceneManager.UnloadSceneAsync(name);
	}

    private IEnumerator Load()
	{
        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if(async.progress > 0.9f)
			{
                async.allowSceneActivation = true;
			}
            yield return null;
        }
        _coroutin = null;
        yield return new WaitForSeconds(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
