using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health;
    float randomX, speed;
    public Vector3 direction;
    [SerializeField] Rigidbody2D rb;
    public GameObject cam;

    void Start()
    {
        speed = 0.1f;
        health = Random.Range(1, 5);
        randomX = Random.Range(-2.35f, 2.35f);
        direction = new Vector3(0f, -5f, randomX) - this.transform.position;

        Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), cam.GetComponent<EdgeCollider2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    public void Damage()
    {
        health -= 1;

        if (health == 0)
        {
            GameManager.score += 1;
            Destroy(this.gameObject);
        }
    }

}
