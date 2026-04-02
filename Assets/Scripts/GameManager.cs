using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject xpOrb;
    public static GameManager instance;

    [Header("Timer")]
    public float matchDuration = 600f; //10 minutos
    private float elapsedTime;
    private bool finished;

    [Header("GameOver")]
    public GameObject gameOverCanvas;

    public TextMeshProUGUI elapsedTimeText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI enemiesText;

    private int enemiesCount;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (finished)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= matchDuration)
        {
            finished = true;
            Win();
        }
    }

    void Win()
    {
        Debug.Log("WIN");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        elapsedTime = 0;
        finished = false;
    }

    public void RestartTimer()
    {
        elapsedTime = 0;
        finished = false;
    }

    public string GetElapsedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        return $"{minutes:00}:{seconds:00}";
    }

    #region RunStats

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        elapsedTimeText.text = GetElapsedTime();
        xpText.text = PlayerController.Instance.GetComponent<PlayerXP>().totalXpEarned.ToString();
        levelText.text = PlayerController.Instance.GetComponent<PlayerXP>().level.ToString();
        enemiesText.text = enemiesCount.ToString();
    }

    public void EnemyCount()
    {
        enemiesCount++;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion

}
