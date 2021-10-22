using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text textHighScore;
    int highScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", highScore);
        textHighScore.text = "High Score: " + highScore;
    }

    public void PLayGame()
    {
        //Load Game
        SceneManager.LoadScene(1);
    }

}
