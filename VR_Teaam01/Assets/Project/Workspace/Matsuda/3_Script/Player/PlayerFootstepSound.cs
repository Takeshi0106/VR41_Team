using UnityEngine;

/*  * プレイヤーの足音を鳴らすスクリプト
 */

[RequireComponent(typeof(PlaySound))]

public class PlayerFootstepSound : MonoBehaviour
{
    [Header("足音のフェード時間")]
    [SerializeField] private float _FadeTime = 0.1f;

    private PlaySound _PlaySound;
    private Vector3 _PreviousPosition;
    private float _StepTimer;
    private bool _IsWalking = false;

    // 初期化
    void Awake()
    {
        _PlaySound = GetComponent<PlaySound>();
        _PreviousPosition = Camera.main.transform.position;
    }

    // 更新処理
    void Update()
    {
        Vector3 currentPosition = Camera.main.transform.position;

        Vector3 movement = currentPosition - _PreviousPosition;
        movement.y = 0.0f;

        bool isWalking = movement.sqrMagnitude > 0.000001f;

        if (isWalking && !_IsWalking)
        {
            Debug.Log("歩き開始");
            _IsWalking = true;
            _PlaySound.Play(true);
        }
        else if (!isWalking && _IsWalking)
        {
            Debug.Log("歩き終了");
            _IsWalking = false;
            _PlaySound.Stop(_FadeTime);
        }

        _PreviousPosition = currentPosition;
    }
}
