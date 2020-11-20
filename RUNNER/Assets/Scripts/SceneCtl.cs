using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
    public static SceneCtl instans = null;

    private Coroutine _coroutin = null;

    [SerializeField]
    private float _fadeTime = 3.0f;

    

	private void Awake()
	{
		if(instans == null)
		{
            instans = this;
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

    // シーン遷移
    public void LoadScene(string name)
	{
        if(name == "")
		{
            return;
		}
        // シーンの切り替え
        // SceneManager.LoadScene(name);

        FadeManager.Instance.LoadScene(name, _fadeTime);
	}

    // シーンの追加
    public void AddScene(string name)
	{
        if (name == "")
        {
            return;
        }
        // シーンの追加
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    // シーン遷移(非同期読み込み処理)
    public void LoadSceneAsync(string name)
	{
        if (name == "")
        {
            return;
        }
		// コルーチンが回っているか調べる
		//if (_coroutin == null)
		//{
		//	_coroutin = StartCoroutine(Load());
		//}
		FadeManager.Instance.LoadSceneAysnc(name, _fadeTime);

    }

    // シーンの削除
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
        // シーン遷移の無効化
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            // 読み込みが完了しているか
            if(async.progress >= 0.9f)
			{
                // シーン遷移の有効化
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
