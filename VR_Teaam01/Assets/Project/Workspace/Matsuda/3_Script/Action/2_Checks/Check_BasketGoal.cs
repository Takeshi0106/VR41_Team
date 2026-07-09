using UnityEditor.PackageManager;
using UnityEngine;

// ==============================================
// 必須コンポーネント定義
// ==============================================
[RequireComponent(typeof(Collider))]


public class Check_BasketGoal : MonoBehaviour
{
    // ==============================================
    // メンバー変数
    // ==============================================
    // アクションを実行するタグ
    [SerializeField] private string m_Tag = "Ball";
    // アクションマネージャー
    [SerializeField] private ActionsManager m_ActionsManager;
    [SerializeField] private BasketBallManager m_BasketBallManager;
    // 当たり判定を設定する
    private Collider m_Collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // エラーチェック
        if(m_ActionsManager == null)
        {
            Debug.LogError("アクションマネージャーが設定されていません");
        }
        if(m_BasketBallManager == null)
        {
            Debug.LogError("BasketBallManagerが設定されていません");
        }

        // トリガーに設定
        m_Collider = GetComponent<Collider>();
        m_Collider.isTrigger = true;
    }

    // ==============================================
    // 当たり判定
    // ==============================================
    void OnTriggerEnter(Collider other)
    {
        // 判定するタグを持つオブジェクトが衝突した場合
        if (other.gameObject.CompareTag(m_Tag))
        {
            // Rigidbody を取得
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            // Y軸の速度を取得
            float verticalVelocity = rb.linearVelocity.y;

            // 下向きなら有効
            if (verticalVelocity > 0.0f)
            {
                return;
            }

            // スコア加算
            m_BasketBallManager.Clear();

            // ゴールログ
            Debug.Log("GOAL!");

            // アクションマネージャーに実行を指示
            m_ActionsManager.ExecuteAction();
        }
    }
}
