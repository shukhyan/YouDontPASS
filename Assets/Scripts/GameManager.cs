using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float random;
    [SerializeField] GameObject enemy;
    bool canInstantiate;
    public static int score, highScore;
    [SerializeField] Text textScore;

    // Start is called before the first frame update
    void Start()
    {
        canInstantiate = true;
        score = 0;
        highScore = PlayerPrefs.GetInt("highscore", highScore);
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = score.ToString();

        if (canInstantiate)
            StartCoroutine(callEnemy());
    }

    //instantiate enemy on a random coordinate on the top of the screen
    IEnumerator callEnemy()
    {
        canInstantiate = false;
        yield return new WaitForSeconds(1.5f);
        random = Random.Range(-2.35f, 2.35f);
        Instantiate(enemy, new Vector3(random, 5f, 0f), enemy.transform.rotation);
        canInstantiate = true;
    }

    public static void resetHighScore()
    {
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

}
