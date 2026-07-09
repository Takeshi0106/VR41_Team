using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/*  * ボールが地面に当たったときにタグを判定してアクションを実行するクラス
 */

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Check_StopBoll : MonoBehaviour
{
    

    // ==============================================
    // メンバー変数
    // ==============================================
    // アクションマネージャー
    [SerializeField] private ActionsManager m_ActionsManager;
    // Rigidbody
    private Rigidbody m_Rb;
    // 持たれているか判定するスクリプト
    private XRGrabInteractable m_GrabInteractable;
    // 離したかのフラグ
    [SerializeField] private bool m_IsRelese = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // エラーチェック
        if (m_ActionsManager == null)
        {
            Debug.LogError("アクションマネージャーが設定されていません");
            return;
        }

        // Rigidbody を取得
        m_Rb = GetComponent<Rigidbody>();
        if (m_Rb == null) 
        {
            Debug.LogError("Rigidbody が設定されていません");
            return;
        }

        // XRGrabInteractable を取得
        m_GrabInteractable = GetComponent<XRGrabInteractable>();
        if (m_GrabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable が設定されていません");
            return;
        }

        // 離したときのイベントを登録
        m_GrabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        // 持たれたときのイベントを削除
        m_GrabInteractable.selectExited.RemoveListener(OnRelease);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsRelese == false)
        {
            return;
        }

        if (m_Rb.linearVelocity.sqrMagnitude < 0.01f)
        {
            // アクションマネージャーに実行を指示
            m_ActionsManager.ExecuteAction();
            m_IsRelese = false;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        m_IsRelese = true;
    }
}
