using UnityEngine;
using Cysharp.Threading.Tasks;

public class GoalCracker : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftCracker;
    [SerializeField] private ParticleSystem rightCracker;
    [Header("パーティクルの再生時間")]
    [SerializeField] private float m_EffectTime = 2.0f;

    public async void PlayGoalEffect()
    {
        leftCracker.Play();
        rightCracker.Play();

        await UniTask.Delay(System.TimeSpan.FromSeconds(m_EffectTime));

        leftCracker.Stop();
        rightCracker.Stop();
    }
}
