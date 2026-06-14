using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [Header("Game UI")]
    public GameObject gameStartUI;
    public GameObject inGameUI;
    public GameObject gameOverUI;
    public GameObject gameClearUI;

    [Header("Score")]
    public Text scoreText;
    public Text totalScoreText;

    [Header("HP")]
    public GameObject heartPrefab;
    public Transform heartParent;
    public Sprite brokenHeart;
    public Sprite fullHeart;
    List<Image> heartImagesList = new List<Image>();

    [Header("Level")]
    public Text stageText;

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
        inGameUI.SetActive(false);
        gameOverUI.SetActive(false);
        gameClearUI.SetActive(false);
    }

    #region  Heart
    public void InitHearts(int maxHp)
    {
        for (int i = 0; i < maxHp; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartParent);

            Image heartImage = newHeart.GetComponent<Image>();
            if (heartImage != null)
            {
                heartImage.sprite = fullHeart;
                heartImagesList.Add(heartImage);
            }
        }
    }

    public void UpdateHp(int currentHp)
    {
        for (int i = 0; i < heartImagesList.Count; i++)
        {
            if (i < currentHp)
            {
                heartImagesList[i].sprite = fullHeart;
            }
            else
            {
                heartImagesList[i].sprite = brokenHeart;
            }
        }
    }

    #endregion

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateTotalScore(int score)
    {
        totalScoreText.text = $"Score: {score}";
    }

    public void UpdateStage(int now)
    {
        // stageText.text = $"Stage : {now + 1}";
    }

    public void ShowGameOver()
    {
        inGameUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void ShowGameClear()
    {
        inGameUI.SetActive(false);
        gameClearUI.SetActive(true);
    }


    #region Buttons
    public void OnClickGameStart()
    {
        GameManager.instance.GameStart();
        gameStartUI.SetActive(false);
        inGameUI.SetActive(true);
    }

    public void OnClickReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickGameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion
}