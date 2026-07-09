using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/*  * バスケットボールのスクリプト
 *  *  * ボールの動きなどを管理
 */

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class BasketBallScript : MonoBehaviour
{
    [Header("投げるときの設定")]
    [SerializeField] private float maxThrowSpeed = 8.0f;
    [SerializeField] private float throwScale = 0.8f;
    [SerializeField] private float yBoost = 1.3f;

    Rigidbody m_Rb;
    XRGrabInteractable m_Grab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Grab = GetComponent<XRGrabInteractable>();

        if (m_Rb == null)
        {
            Debug.LogError("Rigidbodyコンポーネントが見つかりません。");
            return;
        }
        if (m_Grab == null)
        {
            Debug.LogError("Grabコンポーネントが見つかりません。");
            return;
        }

        // 関数登録
        m_Grab.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        m_Grab.selectExited.RemoveListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Vector3 velocity = m_Rb.linearVelocity;

        // 減速させる
        velocity *= throwScale;
        // 山なりにする
        velocity.y += yBoost;

        // 速度上限
        float speed = velocity.magnitude;
        if (speed > maxThrowSpeed)
        {
            velocity = velocity.normalized * maxThrowSpeed;
        }

        m_Rb.linearVelocity = velocity;
    }
}
