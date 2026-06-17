using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Player")]
    public GameObject player;
    PlayerHealth playerHp;
    PlayerMovement playerMove;

    [Header("Hp")]
    public int maxHealth;
    internal int currentHealth;

    [Header("Stage")]
    public GameObject[] stages;
    int stageIndex;

    [Header("Score")]
    public int score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (player != null)
        {
            playerHp = player.GetComponent<PlayerHealth>();
            playerMove = player.GetComponent<PlayerMovement>();
        }
    }

    void Start()
    {
        Time.timeScale = 0f;

        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].SetActive(false);
        }

        player.SetActive(false);
    }

    public void GameStart()
    {
        Time.timeScale = 1f;
        stageIndex = 0;
        currentHealth = maxHealth;

        UIManager.instance.InitHearts(currentHealth);
        UIManager.instance.UpdateStage(stageIndex);
        UIManager.instance.UpdateScore(score);

        stages[stageIndex].SetActive(true);
        player.SetActive(true);
    }

    public void NextStage()
    {
        if (stageIndex < stages.Length - 1)
        {
            stages[stageIndex].SetActive(false);

            stageIndex++;
            stages[stageIndex].SetActive(true);

            UIManager.instance.UpdateStage(stageIndex);
            PlayerReposition();
        }
        else
        {
            Time.timeScale = 0f;
            UIManager.instance.ShowGameClear();
        }
    }

    public void HealthDown()
    {
        if (currentHealth <= 0) { return; }

        currentHealth--;
        UIManager.instance.UpdateHp(currentHealth);

        if (currentHealth <= 0) { GameOver(); }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (playerHp != null) playerHp.OnDie();
        stages[stageIndex].SetActive(false);

        UIManager.instance.ShowGameOver();
    }

    public void PlayerReposition()
    {
        if (playerMove != null)
        {
            playerMove.transform.position = new Vector3(0, 1, 0);
            playerMove.VelocityZero();
        }
    }
}