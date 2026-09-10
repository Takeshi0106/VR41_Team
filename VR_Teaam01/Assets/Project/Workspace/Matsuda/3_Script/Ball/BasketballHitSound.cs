using UnityEngine;

/*  * バスケットボールが当たったときの音を鳴らすスクリプト
 */

[RequireComponent(typeof(PlaySound))]
public class BasketballHitSound : MonoBehaviour
{
    [Header("音を鳴らさないレイヤー")]
    [SerializeField] private LayerMask _IgnoreSoundLayers;

    private PlaySound _PlaySound;

    // 初期化
    void Awake()
    {
        _PlaySound = GetComponent<PlaySound>();
    }

    // 衝突時の処理
    private void OnCollisionEnter(Collision collision)
    {
        // 音を鳴らさないレイヤーに当たった場合は処理しない
        if ((_IgnoreSoundLayers.value & (1 << collision.gameObject.layer)) != 0)
        {
            return;
        }

        // 音を鳴らす
        _PlaySound.Play();
    }
}
