using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Player")]
    public GameObject player;

    [Header("Hp")]
    public int maxHealth;
    internal int currentHealth;

    [Header("Stage")]
    public GameObject[] stages;
    int stageIndex;
    int lastStage;

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
        }
    }

    void Start()
    {
        Time.timeScale = 0f;
        player.SetActive(false);
    }

    public void GameStart()
    {
        Time.timeScale = 1f;
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
            stages[lastStage].SetActive(false);

            stageIndex++;
            stages[stageIndex].SetActive(true);
            lastStage = stageIndex;

            UIManager.instance.UpdateStage(stageIndex);
            PlayerReposition();
        }
        else
        {
            Time.timeScale = 0f;
            UIManager.instance.ShowGameClear();
            UIManager.instance.UpdateTotalScore(score);
        }
    }

    public void HealthDown()
    {
        if (currentHealth > 1)
        {
            currentHealth--;
            UIManager.instance.UpdateHp(currentHealth);
        }
        else { GameOver(); }
    }

    public void GameOver()
    {
        PlayerHealth playerHp = player.GetComponent<PlayerHealth>();

        playerHp.OnDie();
        stages[stageIndex].SetActive(false);

        UIManager.instance.ShowGameOver();
    }

    public void PlayerReposition()
    {
        PlayerMovement playerMove = player.GetComponent<PlayerMovement>();
        playerMove.transform.position = new Vector3(0, 1, 0);
        playerMove.VelocityZero();
    }
}