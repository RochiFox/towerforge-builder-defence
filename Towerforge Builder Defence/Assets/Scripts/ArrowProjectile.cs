using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public static ArrowProjectile Create(Vector3 position, Enemy enemy)
    {
        Transform arrowProjectile = Resources.Load<Transform>("Arrow Projectile");

        Transform arrowTransform = Instantiate(arrowProjectile, position, Quaternion.identity);

        ArrowProjectile arrow = arrowTransform.GetComponent<ArrowProjectile>();
        arrow.SetTarger(enemy);

        return arrow;
    }

    private int damageAmount = 10;

    private Enemy targetEnemy;
    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    private float moveSpeed = 20f;
    private float timeToDie = 2f;

    private void Update()
    {
        if (targetEnemy)
        {
            moveDirection = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDirection = moveDirection;
        }
        else
            moveDirection = lastMoveDirection;

        transform.position += moveSpeed * Time.deltaTime * moveDirection;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDirection));

        timeToDie -= Time.deltaTime;

        if (timeToDie < 0f)
            Destroy(gameObject);
    }

    private void SetTarger(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }

}
