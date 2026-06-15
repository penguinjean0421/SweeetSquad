using UnityEngine;

public class ItemScore : MonoBehaviour, IItemEffect
{
    public int scoreValue;

    public void ApplyEffect()
    {
        GameManager.instance.score += scoreValue;
        UIManager.instance.UpdateScore(GameManager.instance.score);
    }
}
