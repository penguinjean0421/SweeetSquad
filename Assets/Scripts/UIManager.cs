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
    public GameObject gameEndUI;

    [Header("GameEnd")]
    public GameObject gameClear;
    public GameObject gameOver;

    [Header("HP")]
    public GameObject heartPrefab;
    public Transform heartParent;
    public Sprite fullHeart;
    public Sprite brokenHeart;
    List<Image> heartImagesList = new List<Image>();

    [Header("Level")]
    public Text stageText;

    [Header("Score")]
    public Text scoreText;

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

        gameClear.SetActive(false);
        gameOver.SetActive(false);
        gameEndUI.SetActive(false);
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

    public void UpdateStage(int now)
    {
        stageText.text = $"Stage : {now + 1}";
    }

    public void ShowGameOver()
    {
        inGameUI.SetActive(false);
        gameOver.SetActive(true);
        gameEndUI.SetActive(true);

    }

    public void ShowGameClear()
    {
        inGameUI.SetActive(false);
        gameClear.SetActive(true);
        gameEndUI.SetActive(true);

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