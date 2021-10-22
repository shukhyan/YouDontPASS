using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    Transform arrow;
    [SerializeField] GameObject bullet;
    Touch touch = new Touch();
    [SerializeField] float rotationSpeed;
    float rotate;
    int currentWeapon, health;
    bool canShoot, isShooting;
    [SerializeField] Text textHealth, textHighScore;
    [SerializeField] Button wp1Button, wp2Button, wp3Button;
    RaycastHit2D hit;
    LayerMask maskEnemy;

    public GameObject GameOverMenuObj;

    // Start is called before the first frame update
    void Start()
    {
        arrow = transform.GetChild(0);
        currentWeapon = 0;
        canShoot = true;
        isShooting = false;
        health = 5;
        textHealth.text = health.ToString();
        wp1Button.image.color = Color.green;

        GameOverMenuObj.SetActive(false);

        maskEnemy = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        MoveArrow();
        SwitchWeapon();
    }

    void SwitchWeapon()
    {
        hit = Physics2D.Raycast(this.transform.position, arrow.up, Mathf.Infinity, maskEnemy);

        if (hit.collider != null)
        {
            isShooting = true;
        }
        else
            isShooting = false;

        switch (currentWeapon)
        {
            case 0:
                if (isShooting && canShoot)
                    StartCoroutine(Weapon1());
                break;
            case 1:
                if (isShooting && canShoot)
                    StartCoroutine(Weapon2());
                break;
            case 2:
                if (isShooting && canShoot)
                    StartCoroutine(Weapon3());
                break;
        }
    }

    void MoveArrow()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                this.touch = touch;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float delta = this.touch.position.x - touch.position.x;
                rotate += delta * Time.deltaTime * rotationSpeed;
                rotate = Mathf.Clamp(rotate, -45f, 45f);
                arrow.transform.eulerAngles = new Vector3(0f, 0f, rotate);

                this.touch = touch;
            }

            else if (touch.phase == TouchPhase.Ended)
                this.touch = new Touch();
        }
    }

    public void wp1()
    {
        currentWeapon = 0;
        wp1Button.image.color = Color.green;
        wp2Button.image.color = Color.white;
        wp3Button.image.color = Color.white;
    }

    public void wp2()
    {
        if (GameManager.score >= 5)
        {
            currentWeapon = 1;
            wp1Button.image.color = Color.white;
            wp2Button.image.color = Color.green;
            wp3Button.image.color = Color.white;
        }
    }

    public void wp3()
    {
        if (GameManager.score >= 20)
        {
            currentWeapon = 2;
            wp1Button.image.color = Color.white;
            wp2Button.image.color = Color.white;
            wp3Button.image.color = Color.green;
        }
    }

    public IEnumerator Weapon1()
    {
        canShoot = false;
        Instantiate(bullet, this.transform.position, transform.rotation, this.transform);
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    IEnumerator Weapon2()
    {
        canShoot = false;
        Instantiate(bullet, this.transform.position, transform.rotation, this.transform);
        Instantiate(bullet, this.transform.position + arrow.up/4, transform.rotation, this.transform);
        Instantiate(bullet, this.transform.position + arrow.up/2, transform.rotation, this.transform);
        Instantiate(bullet, this.transform.position + 3*arrow.up/4, transform.rotation, this.transform);
        yield return new WaitForSeconds(0.8f);
        canShoot = true;
    }

    public IEnumerator Weapon3()
    {
        canShoot = false;
        Instantiate(bullet, this.transform.position - new Vector3(0.15f, 0f, 0f), transform.rotation, this.transform);
        Instantiate(bullet, this.transform.position - new Vector3(0.35f, 0.2f, 0f), transform.rotation, this.transform);
        Instantiate(bullet, this.transform.position - new Vector3(0.55f, 0.4f, 0f), transform.rotation, this.transform);
        Instantiate(bullet, this.transform.position + new Vector3(0.15f, 0f, 0f), transform.rotation, this.transform);
        Instantiate(bullet, this.transform.position + new Vector3(0.35f, -0.2f, 0f), transform.rotation, this.transform);
        Instantiate(bullet, this.transform.position + new Vector3(0.55f, -0.4f, 0f), transform.rotation, this.transform);

        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            health -= 1;
            textHealth.text = health.ToString();
            Destroy(collision.gameObject);

            
            if (health <= 0)
            {
                Time.timeScale = 0;
                GameManager.resetHighScore();

                gameOverMenu();
            }
            
        }
    }

    public void gameOverMenu()
    {
        textHighScore.text = "High Score: " + GameManager.highScore.ToString();
        GameOverMenuObj.SetActive(true);
    }

}
