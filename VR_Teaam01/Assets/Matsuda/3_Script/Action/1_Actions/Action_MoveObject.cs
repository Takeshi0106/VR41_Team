using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class Action_MoveObject : BaseAction
{
    // ==============================================
    // メンバー変数
    // ==============================================
    [SerializeField] private Transform m_TargetObject = null;
    [SerializeField] private BasketBallManager m_Manager;
    [SerializeField] private Vector3 m_MovePos = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(m_TargetObject == null)
        {
            Debug.LogWarning("ターゲットオブジェクトが設定されていません。自身のオブジェクトをターゲットにします。");
            m_TargetObject = transform;
        }
        if(m_Manager == null)
        {
            Debug.LogError("BasketBallManagerがアタッチされていません。");
            return;
        }
    }


    public override UniTask Execute(CancellationToken token)
    {
        m_Manager.Challenge();
        transform.position = m_MovePos;
        return UniTask.CompletedTask;
    }
}
