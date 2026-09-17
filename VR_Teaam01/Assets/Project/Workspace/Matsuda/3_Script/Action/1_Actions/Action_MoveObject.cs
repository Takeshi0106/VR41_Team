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
    [SerializeField] private float m_Distance = 2.0f;
    
    private Transform m_Camera;

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
        m_Camera = Camera.main.transform;
    }


    public override UniTask Execute(CancellationToken token)
    {
        m_Manager.Challenge();

        Vector3 forward = m_Camera.forward;
        Vector3 targetPosition = m_Camera.position + forward * m_Distance;
        m_TargetObject.position = targetPosition;
        
        Debug.Log("初期位置に戻しました。");
        return UniTask.CompletedTask;

    }
}
