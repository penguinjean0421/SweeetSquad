using UnityEngine;

public class ItemScore : MonoBehaviour, IItemEffect
{
    public int scoreValue;

    public void ApplyEffect()
    {
        // 파티클 넣기

        GameManager.instance.score += scoreValue;
        UIManager.instance.UpdateScore(GameManager.instance.score);
    }
}
