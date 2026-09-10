using UnityEngine;

/*  * シーン開始時にBGMを再生するスクリプト
 */

public class PlayBGMOnStart : MonoBehaviour
{
    [Header("再生するBGMの種類")]
    [SerializeField] private BGMType _BGMType = BGMType.Title;
    [Header("BGMの音量")]
    [SerializeField, Range(0.0f, 1.0f)] private float _Volume = 1.0f;
    [Header("BGMのフェード時間")]
    [SerializeField] private float _FadeTime = 1.0f;

    private SoundManager _SoundManager;

    private void Awake()
    {
        _SoundManager = SoundManager.GetInstance();
    }

    void Start()
    {
        _SoundManager.PlayBgm(_BGMType, _FadeTime, _Volume);
    }
}
