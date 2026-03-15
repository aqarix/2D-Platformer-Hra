using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] float explosionRadius = 3f;
    [SerializeField] float explosionForce = 15f;
    [SerializeField] GameObject explosionEffect;

    private Rigidbody2D rb;
    AudioSource asc;
    [SerializeField] AudioClip Explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        asc = GetComponent<AudioSource>();

        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            return;
        }

        asc.PlayOneShot(Explosion);
        Explode();
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D obj in objectsInRange)
        {
            Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();

            if (objRb != null)
            {
                Vector2 direction = (obj.transform.position - transform.position).normalized;

                objRb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}