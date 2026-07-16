using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* @file サウンド管理マネージャー
 * @brief サウンドを途切れないように保持する
 * @memo サウンドをこのマネージャーを通して再生し、
 * 　　　シーン遷移しても途切れないようにする
 * 　　　プレハブオブジェクトに設定して、名前から再生を行います
 * 　　　PlayBGMのフェード時間はフェードアウトと併せた時間です。
 * 　　　
 * 　　　PlayBGMの第一引き数は再生する音の名前、
 * 　　　第二引き数はフェードするまでの時間です。
 * 　　　第三引き数は音量でデフォルトは1.0
 * 　　　ただ音楽側でそろえた方が良いと思います。
*/

public class SoundManager : MonoBehaviour
{
    [Header("BGM登録")]
    [SerializeField] private AudioClip[] m_BgmClips;
    [Header("SE登録")]
    [SerializeField] private AudioClip[] m_SeClips;

    private static SoundManager m_Instance;
    private Dictionary<string, AudioClip> m_BgmDict;
    private Dictionary<string, AudioClip> m_SeDict;
    private AudioSource m_BgmSource;
    private AudioSource m_SeSource;

    // ==================================
    // 開始処理
    // ==================================
    void Awake()
    {
        // すでにオブジェクトが生成されたら自分を削除する
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        m_Instance = this;
        // シーンを跨いでも削除されないように設定
        DontDestroyOnLoad(gameObject);

        // AudioSource作成
        m_BgmSource = gameObject.AddComponent<AudioSource>();
        m_SeSource = gameObject.AddComponent<AudioSource>();

        // 設定
        m_BgmSource.loop = true;
        m_BgmSource.playOnAwake = false;
        m_SeSource.playOnAwake = false;

        // 音量を下げておく
        m_BgmSource.volume = 0.001f;
        m_SeSource.volume = 0.001f;

        // ハッシュ配列に登録
        m_BgmDict = new Dictionary<string, AudioClip>();
        foreach (var clip in m_BgmClips)
        {
            if (clip != null)
            {
                // 名前をキーに登録
                m_BgmDict[clip.name] = clip;

                // デコード処理 (一度再生して止める)
                m_BgmSource.clip = clip;
                m_BgmSource.Play();
                m_BgmSource.Pause(); 
            }
        }

        // ハッシュ配列に登録
        m_SeDict = new Dictionary<string, AudioClip>();
        foreach (var clip in m_SeClips)
        {
            if (clip != null)
            {
                // 名前をキーに登録
                m_SeDict[clip.name] = clip;

                // デコード処理 (一度再生して止める)
                m_SeSource.clip = clip;
                m_SeSource.Play();
                m_SeSource.Pause();
            } 
        }

        // 音量を元に戻す
        m_BgmSource.volume = 1.0f;
        m_SeSource.volume = 1.0f;
    }

    // =================================
    // BGMの処理
    // =================================
    public void PlayBgm(string _name, float _fadeTime, float _maxVolume = 1.0f)
    {
        // 名前から検索
        if (!m_BgmDict.TryGetValue(_name, out var clip))
        {
            Debug.LogWarning($"指定されたBGM {_name} が存在しません。");
            return;
        }

        // 同じ曲なら再生し直さない
        if (m_BgmSource.clip == clip && m_BgmSource.isPlaying) { return; }

        // 音を切り替える
        StartCoroutine(ChangeBgm(clip, _fadeTime, _maxVolume));
    }

    //===============================
    // BGM停止
    //===============================
    public void StopBgm(float _fadeTime = 0.0f)
    {
        // フェードアウトを実行
        StartCoroutine(SoundFadeOut(_fadeTime));
    }

    //===============================
    // SE再生
    //===============================
    public void PlaySe(string _name)
    {
        // 名前でSEを検索すしてクリップを取得
        if (!m_SeDict.TryGetValue(_name, out var clip))
        {
            Debug.LogWarning($"指定されたSE {_name} が存在しません。");
            return;
        }
        
        // SEを複数鳴らすように設定
        m_SeSource.PlayOneShot(clip);
    }

    //===============================
    // SE停止
    //===============================
    public void StopSe()
    {
        // SEを停止させる
        m_SeSource.Stop();
    }

    // ================================
    //  自分の実態を渡すゲッター
    // ================================
    public static SoundManager GetInstance()
    {
        // 実態がなければ生成
        if (m_Instance == null)
        {
            // Prefabをロード
            var prefab = Resources.Load<GameObject>("Matuda/SoundManager");
            // オブジェクトを生成
            var obj = Object.Instantiate(prefab);
            // アタッチされているコンポーネントを取得
            m_Instance = obj.GetComponent<SoundManager>();

            // シーン遷移時に破棄されないようにする
            DontDestroyOnLoad(obj);

            // ログ出力
            Debug.Log("サウンドマネージャー作成");
        }

        return m_Instance;
    }

    // ==========================================
    // オーディオのフェードイン
    // ==========================================
    private IEnumerator SoundFadeIn(float _fadeTime, float _maxVolume)
    {
        // 音を最初に再生
        m_BgmSource.volume = 0f;
        m_BgmSource.Play();

        // 経過時間を保持
        float time = 0.0f;

        // フェード時間が経過するまで
        while (time < _fadeTime)
        {
            // 経過時間を取得
            time += Time.deltaTime;

            // 音の大きさを上げる
            m_BgmSource.volume = Mathf.Lerp(0f, _maxVolume, time / _fadeTime);

            // 1フレームまつ
            yield return null;
        }

        Debug.Log("再生開始");

        // 念のため音量を最大に固定
        m_BgmSource.volume = _maxVolume;
    }

    // ==========================================
    // オーディオのフェードアウト
    // ==========================================
    private IEnumerator SoundFadeOut(float _fadeTime)
    {
        // 音の大きさを取得
        float startVolume = m_BgmSource.volume;

        // 時間経過を保持
        float time = 0.0f;
        
        // 比較
        while (time < _fadeTime)
        {
            // 経過時間を取得
            time += Time.deltaTime;

            // 音の大きさを下げる
            m_BgmSource.volume = Mathf.Lerp(startVolume, 0f, time / _fadeTime);

            // 1フレーム待つ
            yield return null;
        }

        Debug.Log("終了開始");

        // 音の大きさを0にして音を止める
        m_BgmSource.volume = 0f;
        m_BgmSource.Stop();

        // 音の大きさを元に戻す
        m_BgmSource.volume = startVolume;
    }

    // ==========================================
    // オーディオのチェンジ
    // ==========================================
    private IEnumerator ChangeBgm(AudioClip _newClip, float _fadeTime, float _maxVolume)
    {
        // 関数が終わるまで待つ
        if (m_BgmSource.isPlaying)
        {
            // Debug.Log("BGMフェードアウト開始");
            yield return StartCoroutine(SoundFadeOut(_fadeTime * 0.5f));
            // Debug.Log("BGMフェードアウト完了");
        }

        // セット
        m_BgmSource.clip = _newClip;
        m_BgmSource.volume = 0.0f;
        m_BgmSource.Play();

        // Debug.Log($"BGM再生開始: {_newClip.name}");

        // フェードイン
        yield return StartCoroutine(SoundFadeIn(_fadeTime * 0.5f, _maxVolume));

        // Debug.Log($"BGMフェードイン完了: {_newClip.name}");
    }

    // ==========================================
    // エディター上に設置したら警告を出す
    // ==========================================
#if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying && gameObject.scene.name != null)
        {
            Debug.LogError("[SoundManager] シーンに直接配置しないでください！自動生成されます。");
        }
    }
#endif
}
