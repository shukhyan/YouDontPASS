using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float random;
    [SerializeField] GameObject Enemy;
    bool canInstanciate;
    public static int score, highScore, levelUp;
    [SerializeField] Text textScore;

    // Start is called before the first frame update
    void Start()
    {
        levelUp = 0;
        canInstanciate = true;
        score = 0;
        highScore = PlayerPrefs.GetInt("highscore", highScore);
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = score.ToString();

        if (canInstanciate)
            StartCoroutine(callEnemy());
    }

    IEnumerator callEnemy()
    {
        canInstanciate = false;
        yield return new WaitForSeconds(1.5f);
        random = Random.Range(-2.35f, 2.35f);
        Instantiate(Enemy, new Vector3(random, 5f, 0f), Enemy.transform.rotation);
        canInstanciate = true;
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
