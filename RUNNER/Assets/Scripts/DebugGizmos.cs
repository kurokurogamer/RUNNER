using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回転対応でバック表示用Gizmosクラス
public class DebugGizmos : MonoBehaviour
{
    // オブジェクトタイプ
    private enum OBJECT_TYPE
	{
        CUBE,
        SPHERE,
        MAX
	}

    [SerializeField, Tooltip("デバックモードの有無")]
    private bool _debugMode = true;

    [SerializeField,Tooltip("Gizmosカラー")]
    private Color _color = Color.white;
    [SerializeField, Tooltip("オブジェクトタイプ")]
    private OBJECT_TYPE _mode = OBJECT_TYPE.CUBE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnDrawGizmos()
	{
        // デバックモードでなければ処理を終了する
        if(!_debugMode)
		{
            return;
		}
        // カラーの変更
        Gizmos.color = _color;
        // 対象オブジェクトの行列を取得
		Matrix4x4 matrix = transform.localToWorldMatrix;
        // Gizmosの行列に代入
		Gizmos.matrix = matrix;
		// 描画
		switch (_mode)
		{
			case OBJECT_TYPE.CUBE:
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                break;
			case OBJECT_TYPE.SPHERE:
                Gizmos.DrawSphere(Vector3.zero, 0.5f);
                break;
			case OBJECT_TYPE.MAX:
			default:
				break;
		}
	}
}
