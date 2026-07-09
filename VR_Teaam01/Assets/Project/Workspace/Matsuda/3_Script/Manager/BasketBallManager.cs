using UnityEngine;

public class BasketBallManager : MonoBehaviour
{
    [SerializeField] private int m_Score = 0;
    [SerializeField] private int m_ChallengeCount = 10;

    public void Challenge()
    {
        m_ChallengeCount--;
        if (m_ChallengeCount <= 0)
        {
            Debug.Log("チャレンジ回数が終了しました。");
            GameResultData.ClearCount = m_Score;
            SceneTransitionManager.GetInstance().SceneTransition(SCENETYPE.RESULT);
        }
    }

    public void Clear()
    {
        m_Score++;
        Debug.Log("スコア: " + m_Score);
    }
}
