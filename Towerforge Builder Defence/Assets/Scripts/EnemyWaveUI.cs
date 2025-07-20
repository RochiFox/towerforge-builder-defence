using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;

    private Camera mainCamera;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    private RectTransform enemyWaveSpawnPointIndicator;
    private RectTransform enemyClosestPositionIndicator;

    private void Awake()
    {
        waveNumberText = transform.Find("Wave Number Text").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("Wave Message Text").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnPointIndicator = transform.Find("Enemy Wave Spawn Position Indicator").GetComponent<RectTransform>();
        enemyClosestPositionIndicator = transform.Find("Enemy Closest Position Indicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManagerOnWaveNumberChanged;
    }

    private void Update()
    {
        HandleNextWaveMessage();
        HandleEnemyWaveSpawnPositionIndicator();
        HandleEnemyClosestPositionIndicator();
    }

    private void EnemyWaveManagerOnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();

        if (nextWaveSpawnTimer <= 0f)
            SetMessageText("");
        else
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
    }

    private void HandleEnemyWaveSpawnPositionIndicator()
    {
        Vector3 directionToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;

        enemyWaveSpawnPointIndicator.anchoredPosition = directionToNextSpawnPosition * 300f;
        enemyWaveSpawnPointIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(directionToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPointIndicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);
    }

    private void HandleEnemyClosestPositionIndicator()
    {
        float targetMaxRadius = 9999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);

        Enemy targetEnemy = null;

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();

            if (enemy)
            {
                if (!targetEnemy)
                    targetEnemy = enemy;
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }

        if (targetEnemy)
        {
            Vector3 directionToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;

            enemyClosestPositionIndicator.anchoredPosition = directionToClosestEnemy * 250f;
            enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(directionToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);
            enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
        }
        else
        {
            enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }

    private void SetMessageText(string message)
    {
        waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}
