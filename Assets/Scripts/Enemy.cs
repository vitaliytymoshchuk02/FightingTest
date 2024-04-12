using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private GameObject prefabExplosion;
    [SerializeField] private int health;
    [SerializeField] private int damage = 100;

    private GameManager gameManager;
    private Vector2 currentPoint;
    private Vector2 targetPoint;
    private float timeToTargetPoint;
    private float elapsedTime;
    private float distanceOffset = 0.1f;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        transform.position += positionOffset;
        TargetPoint();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!gameManager.GetGameEnded())
        {
            elapsedTime += Time.deltaTime;
            float elapsedPercentage = elapsedTime / timeToTargetPoint;
            transform.position = Vector3.Lerp(currentPoint, targetPoint, elapsedPercentage);

            if (elapsedPercentage >= 1 - distanceOffset)
            {
                gameManager.FinishGame();
            }
        }
    }

    private void TargetPoint()
    {
        currentPoint = transform.position;
        targetPoint = new Vector3(targetObject.position.x, currentPoint.y);
        
        float distanceToPoint = Vector3.Distance(currentPoint, targetPoint);
        timeToTargetPoint = distanceToPoint / speed;

        elapsedTime = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Hit();
        }
    }

    private void Hit()
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        gameManager.IncreaseKillCounter();

        GameObject explosion = Instantiate(prefabExplosion, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        Destroy(gameObject);
    }
}
