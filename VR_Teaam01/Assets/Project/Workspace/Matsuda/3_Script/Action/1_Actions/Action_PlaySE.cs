using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

/*  *SEを再生するアクション
 */

public class Action_PlaySE : BaseAction
{
    [Header("再生するSEの種類")]
    [SerializeField] private SEType m_SeType;

    private SoundManager m_SoundManager;

    // 初期化
    void Awake()
    {
        m_SoundManager = SoundManager.GetInstance();
    }

    // アクション実行
    public override UniTask Execute(CancellationToken token)
    {
        m_SoundManager.PlaySe(m_SeType);
        return UniTask.CompletedTask;
    }
}
