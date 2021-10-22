using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float speed = 15f;
    Vector3 direction;
    Transform arrow;

    void Start()
    {
        arrow = this.transform.parent.GetChild(0);
        direction = arrow.transform.up;
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Enemy>().Damage();
            Destroy(this.gameObject);
        }

        if (collision.tag == "MainCamera")
            Destroy(this.gameObject);
    }

}