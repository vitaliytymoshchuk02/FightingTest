using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPointRight;
    [SerializeField] private Transform shootPointDuckRight;
    [SerializeField] private Transform shootPointLeft;
    [SerializeField] private Transform shootPointDuckLeft;
    [SerializeField] private float speed = 10f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
            animator.SetTrigger("Shoot");
            Shoot(shootPointLeft);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
            animator.SetTrigger("Shoot");
            Shoot(shootPointRight);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetTrigger("Duck");
            if (spriteRenderer.flipX == true)
            {
                Shoot(shootPointDuckLeft);
            }
            else
            {
                Shoot(shootPointDuckRight);
            }
        }
    }

    private void Shoot(Transform shootPoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        rb.velocity = spriteRenderer.flipX ? Vector2.left * speed : Vector2.right * speed;
    }
}
