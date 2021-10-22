using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "High Score: " + GameManager.highScore.ToString();
    }

    public void PLayGame()
    {
        SceneManager.LoadScene(1);
    }

}
