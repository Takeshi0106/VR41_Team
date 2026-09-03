using UnityEngine;

public class GoalCracker : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftCracker;
    [SerializeField] private ParticleSystem rightCracker;

    public void PlayGoalEffect()
    {

        leftCracker.Play();
        rightCracker.Play();
    }
}
