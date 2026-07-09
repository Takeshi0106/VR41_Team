using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;


/*  * アクション処理クラス
 *  * デバッグ用のログを表示するアクション
 */

public class Action_DebugLog : BaseAction
{
    // ===========================================
    // メンバー変数
    // ===========================================
    // ログメッセージ
    [SerializeField] private string m_LogMessage = "アクション処理が実行されました。";

    // ===========================================
    // デバッグアクション処理
    // ===========================================
    public override UniTask Execute(CancellationToken token)
    {
        Debug.Log(m_LogMessage);
        return UniTask.CompletedTask;
    }
}
