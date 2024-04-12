using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TextMeshProUGUI killCounterText;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private TextMeshProUGUI totalKillsText;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Player player;
    [SerializeField] private GameObject playerExplosionPrefab;
    [SerializeField] private Transform playerExplosionPosition;

    private int killCounter;
    private int bestResult;
    private bool gameEnded = false;
    private float explosionDuration = 0.8f;

    public void FinishGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;

            enemySpawner.Pause();
            ShowPlayerExplosion();
            UpdateBest();
            StartCoroutine(ShowMenu());
        }
    }

    private void ShowPlayerExplosion()
    {
        GameObject explosion = Instantiate(playerExplosionPrefab, playerExplosionPosition);
        Destroy(explosion, explosionDuration);
        Destroy(player.gameObject);
    }

    private IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(explosionDuration);
        gameOverMenu.SetActive(true);
    }

    private void UpdateBest()
    {
        bestResult = PlayerPrefs.GetInt("best");
        if (killCounter > bestResult)
        {
            bestResult = killCounter;
            PlayerPrefs.SetInt("best", bestResult);
        }
        UpdateBestUI();
    }

    private void UpdateBestUI()
    {
        totalKillsText.text = "Total kills: " + killCounter;
        bestText.text = "Best: " + bestResult;
    }

    public void IncreaseKillCounter()
    {
        killCounter++;
        killCounterText.text = killCounter.ToString();
    }

    public bool GetGameEnded() => gameEnded;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

    }
}
