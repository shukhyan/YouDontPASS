using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text textHighScore;

    // Start is called before the first frame update
    void Start()
    {
        textHighScore.text = "High Score: " + GameManager.highScore.ToString();
    }

    public void PLayGame()
    {
        //Load Game
        SceneManager.LoadScene(1);
    }

}
