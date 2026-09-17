using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

/* @file サウンド再生
*/

public class PlaySound : MonoBehaviour
{
    [Header("再生する音")]
    [SerializeField] private AudioClip _Clip;
    [Header("再生する音声")]
    [SerializeField] private AudioSource _Source;
    [Header("音量")]
    [SerializeField, Range(0.0f, 1.0f)] private float _Volume = 1.0f;

    private bool _isPlaying = false;

    // 初期化
    void Awake()
    {
        if(_Source == null)
        {
            Debug.LogWarning("AudioSourceが設定されていません。");
            _Source = gameObject.GetComponent<AudioSource>();
            if(_Source == null)
            {
                Debug.LogError("AudioSourceが取得できませんでした。");
            }
        }
        if(_Clip == null)
        {
            Debug.LogError("AudioClipが設定されていません。");
        }
        else
        {
            _Source.clip = _Clip;
            _Source.volume = _Volume;
        }
    }

    // 再生
    public void Play(bool loop = false)
    {
        Debug.Log($"PlaySound.Play() called. AudioClip: {_Clip.name}");

        if (!_isPlaying)
        {
            _isPlaying = true;
            _Source.volume = _Volume;
            _Source.loop = loop;
            _Source.Play();

            if (!loop)
            {
                WaitForPlayEnd().Forget();
            }
        }
    }

    // サウンド終了
    public void Stop(float fadeTime)
    {
        Debug.Log($"PlaySound.Stop() called. AudioClip: {_Clip.name}");
        StopSound(fadeTime).Forget();
    }

    // 再生終了待機
    private async UniTaskVoid WaitForPlayEnd()
    {
        await UniTask.WaitUntil(() => !_Source.isPlaying);

        _isPlaying = false;
    }

    // サウンド終了処理
    private async UniTaskVoid StopSound(float fadeTime)
    {
        float startVolume = _Source.volume;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;

            float rate = elapsedTime / fadeTime;
            _Source.volume = Mathf.Lerp(startVolume, 0.0f, rate);

            await UniTask.Yield();
        }

        _Source.Stop();

        _Source.volume = startVolume;
        _isPlaying = false;
    }
}
