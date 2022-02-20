using UnityEngine;

using UnityEngine.UI;
public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;

    int scoreAmount = 50;
    int score = -1; //In beginning of the first tour, score will be -1
    [Header("UI Texts")]
    [SerializeField] Text scoreText;
    [SerializeField] Text currentScoreText;
    [SerializeField] Text highScoreText;

    void Awake()
    {
        //Singleton
        if(instance == null)
            instance = this;
    }

    public void UpdateScore()
    {
        score += scoreAmount;
        scoreText.text = score.ToString();
    }
    //Score amount is right proportion with distance between goal and the player
    public void SetScoreAmount(Vector3 playerBodyPosition, Vector3 currentPlace)
    {
        scoreAmount = (int) (Vector3.Magnitude(playerBodyPosition - currentPlace) * 10);
    }

    public void RefreshingHighScore()
    {
        //Refresh score if it has been broken
        if(score > PlayerPrefs.GetInt("highScore"))
            PlayerPrefs.SetInt("highScore", score);
    }

    //To show scores on Texts after game is ended
    public void ScoreTable()
    {
        //High Score
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
        highScoreText.gameObject.SetActive(true);

        //Score - if we are not beginning of first tour 
        if(score != -1)
        {
            currentScoreText.text = "Score: " + score;
            currentScoreText.gameObject.SetActive(true);
        }
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "0";
    }
    //For just beginning of the first tour
    public void WriteHighScoreInitially()
    {
        if(PlayerPrefs.GetInt("highScore") > 0)
        {
            highScoreText.text= "High Score: " + PlayerPrefs.GetInt("highScore");
            highScoreText.gameObject.SetActive(true);
        }
    }

    public void HideLastHighScoreTables()
    {
        highScoreText.gameObject.SetActive(false);
        currentScoreText.gameObject.SetActive(false);
    }
}
