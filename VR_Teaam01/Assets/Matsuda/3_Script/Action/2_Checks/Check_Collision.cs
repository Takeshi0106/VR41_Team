using UnityEngine;


/*  * 物体と当たったときにタグを判定してアクションを実行するクラス
 */


// ==============================================
// 必須コンポーネント定義
// ==============================================
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ActionsManager))]


public class Check_Collision : MonoBehaviour
{
    // ==============================================
    // メンバー変数
    // ==============================================
    // アクションを実行するタグ
    [SerializeField] private string m_Tag = "Player";
    // 当たり判定を設定する
    [SerializeField] private Collider m_Collider;
    // アクションマネージャー
    private ActionsManager m_ActionsManager;


    // ==============================================
    // 初期化処理
    // ==============================================
    void Start()
    {
        // アクションマネージャーを取得
        m_ActionsManager = GetComponent<ActionsManager>();

        // トリガーに設定
        m_Collider.isTrigger = false;
    }


    // ==============================================
    // アクション判定処理
    // ==============================================
    void OnCollisionEnter(Collision other)
    {
        // 判定するタグを持つオブジェクトが衝突した場合
        if (other.gameObject.CompareTag(m_Tag))
        {
            // アクションマネージャーに実行を指示
            m_ActionsManager.ExecuteAction();
        }
    }
}
