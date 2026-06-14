using UnityEngine;

public class ItemHp : MonoBehaviour, IItemEffect
{
    public GameManager gameManager;
    public UIManager uiManager;

    public int HpValue;

    public void ApplyEffect()
    {
        if (GameManager.instance.currentHealth < GameManager.instance.maxHealth)
        {
            GameManager.instance.currentHealth = Mathf.Min(GameManager.instance.currentHealth + HpValue, GameManager.instance.maxHealth);
        }
        else
        {
            // 캐릭터 재질 바꾸고

            // 3초동안 이속 1.5배 + 무적
        }

        UIManager.instance.UpdateHp(GameManager.instance.currentHealth);
    }
}

