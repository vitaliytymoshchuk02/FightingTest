using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject prefabHit;
    private float lifespan = 2f;
    void Start()
    {
        Destroy(gameObject, lifespan);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            InstantiateHitEffect();
            Destroy(gameObject);
        }
    }
    private void InstantiateHitEffect()
    {
        if (prefabHit != null)
        {
            GameObject hitEffect = Instantiate(prefabHit, transform.position, Quaternion.identity);
            Destroy(hitEffect, 0.5f);
        }
    }
}
