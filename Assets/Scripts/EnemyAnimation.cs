using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateWalkingAnimation(int nextMove)
    {
        anim.SetInteger("WalkSpeed", nextMove);

        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == 1);
        }
    }

    public void PlayDeathVisuals()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
    }
}