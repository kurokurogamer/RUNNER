using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFade : MonoBehaviour
{
    // アニメーション用変数
    private Animator _animator;
    private Button _button;
    private Text _text;
    private List<Button> _buttonList = new List<Button>();
    private List<Text> _textList = new List<Text>();
    [SerializeField]
    private string _name = "";

    [SerializeField]
    private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
        foreach (Transform child in transform)
		{
            _text = child.GetComponent<Text>();
		}
        // 親から全ボタンの取得
        foreach(Transform trans in transform.root)
		{
            // Buttonがコンポーネントされているか確認(nullチェック)
            if (trans.TryGetComponent(out Button button))
            {
                // 自分以外ならリストに追加
                if (_button != button)
                {
                    _buttonList.Add(button);
                }
            }

            foreach (Transform child in trans)
            {
                // Textがコンポーネントされているか確認(nullチェック)
                if (child.TryGetComponent(out Text text))
                {
                    // 自分以外ならリストに追加
                    if (_text != text)
                    {
                        _textList.Add(text);
                    }
                }
            }
        }
    }

    public void ButtonPush()
	{
        _animator.SetBool("Fade", true);
        _text.material = null;
        for(int i = 0; i < _buttonList.Count; i++)
		{
            AudioManager.instans.PlaySE(clip);

            _textList[i].material = null;
            _buttonList[i].interactable = false;
		}
        if (_name != "")
        {
            

            SceneCtl.instans.LoadSceneAsync(_name);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}