using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

/*  * ゴールした時のアクション
 */

[RequireComponent(typeof(GoalCracker))]
public class Action_GoalEffect : BaseAction
{
    private GoalCracker m_GoalCracker;

    // 初期化
    void Awake()
    {
        m_GoalCracker = GetComponent<GoalCracker>();
    }

    // アクション実行
    public override UniTask Execute(CancellationToken token)
    {
        m_GoalCracker.PlayGoalEffect();
        return UniTask.CompletedTask;
    }
}
