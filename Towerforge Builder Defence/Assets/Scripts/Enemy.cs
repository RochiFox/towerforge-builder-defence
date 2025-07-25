using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        Transform Enemy = Resources.Load<Transform>("Enemy");

        Transform enemyTransform = Instantiate(Enemy, position, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();

        return enemy;
    }

    private Transform targetTransform;
    private Rigidbody2D rigidbody2d;
    private HealthSystem healthSystem;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = 0.2f;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        if (BuildingManager.Instance.GetHQBuilding() != null)
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;

        healthSystem.OnDied += HealthSystemOnDied;

        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargeting();
    }

    private void HealthSystemOnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 10f;

        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();

            if (building)
            {
                if (!targetTransform)
                    targetTransform = building.transform;
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, targetTransform.position))
                    {
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null)
        {
            if (BuildingManager.Instance.GetHQBuilding() != null)
                targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
    }

    private void HandleMovement()
    {
        if (targetTransform)
        {
            Vector3 moveDirection = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 6f;

            rigidbody2d.velocity = moveDirection * moveSpeed;
        }
        else
            rigidbody2d.velocity = Vector2.zero;
    }

    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime;

        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();

        if (building)
        {
            healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);

            Destroy(gameObject);
        }
    }
}
