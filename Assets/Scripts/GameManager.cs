using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
    {
    public Text ContinueTimerText;
    public Text PointsText;
    public Text GameOverPointsText;
    public Text GameOverHighScoreText;
    int numberOfGames;
    public Camera camera;
    public GameObject PressableColor;
    int PressNumber;
    public ExpandingBallScript Ball;
    public List<Color> colors;
    public int Points,HighScore;
    public GameObject GameOverPanel;
    public GameObject SecondChancePanel;
    // Start is called before the first frame update
    void Start()
        {
        numberOfGames = 0;//Counter of games, every 3 games the game dispalys an ad
        if (numberOfGames < PlayerPrefs.GetInt("Games"))
            numberOfGames = PlayerPrefs.GetInt("Games");
        PressNumber = OpenSceneManager.level * 2 + 4;
        Points = -1;
        HighScore = PlayerPrefs.GetInt("HighScore" + OpenSceneManager.level.ToString());
        Ball = GameObject.Find("Maincircle").GetComponent<ExpandingBallScript>();
        CreateButtons();
        }
    public void CreateButtons()
        {
        float left = (PressNumber + PressNumber % 2) / 2;
        float right = (PressNumber - PressNumber % 2) / 2;
        for (float i = 0; i < left; i++)
            {
            GameObject Button = Instantiate(PressableColor, new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width / 10, 0, 0)).x, (camera.orthographicSize+1) - ((camera.orthographicSize + 1)*2 / (left + 1)) * (i + 1)), transform.rotation);
            GenerateButtonColor(Button);
            }
        for (float i = 0; i < right; i++)
            {
            GameObject Button = Instantiate(PressableColor, new Vector2(camera.ScreenToWorldPoint(new Vector3(9 * Screen.width / 10, 0, 0)).x, (camera.orthographicSize + 1) - ((camera.orthographicSize + 1) * 2 / (right + 1)) * (i + 1)), transform.rotation);
            GenerateButtonColor(Button);
            }
        }
    public void GenerateButtonColor(GameObject Button)
        {
        int rnd = Random.Range(0, colors.Count);
        Button.GetComponent<SpriteRenderer>().color = colors[rnd];
        Ball.Colors.Add(colors[rnd]);
        colors.RemoveAt(rnd);
        }
    public void ChangePoints(int change)
        {
        Points += change;
        PointsText.text = Points.ToString();
        }
    public void GameOver()
        {
        SecondChancePanel.SetActive(false);
        Time.timeScale = 1;
        GameOverPanel.SetActive(true);
        GameOverPointsText.text = "You Scored: " + System.Environment.NewLine + Points + " points!";
        if (Points > HighScore)
            HighScore = Points;
        PlayerPrefs.SetInt("HighScore" + OpenSceneManager.level.ToString(), HighScore);
        GameOverHighScoreText.text = "Your high score" + System.Environment.NewLine + " in this level is: " + System.Environment.NewLine + HighScore + " points!";
        GameObject[] pressables = GameObject.FindGameObjectsWithTag("Pressable");
        for (int i = 0; i < pressables.Length; i++)
            Destroy(pressables[i]);
        Destroy(Ball.gameObject);
        }
    public void Restart()
        {
        numberOfGames++;
        if (numberOfGames == 3)
            {
            if (Advertisement.IsReady())
                Advertisement.Show("Interstitial_Android", new ShowOptions() { resultCallback = InterstitialOutCome });
            }
            PlayerPrefs.SetInt("Games", numberOfGames);
            SceneManager.LoadScene("OpenScene");
        }
    public void InterstitialOutCome(ShowResult result)
        {
        if(result == ShowResult.Finished||result==ShowResult.Skipped)
            numberOfGames -= 3;
        }
    public void SecondChanceOption()
        {
        Time.timeScale = 0;
        Ball._gameRunning = false;
        SecondChancePanel.SetActive(true);
        }
    public void AdSecondChance()
        {
        SecondChancePanel.SetActive(false);
        Time.timeScale = 1;
        if (Advertisement.IsReady())
            Advertisement.Show("Rewarded_Android", new ShowOptions() { resultCallback = SecondChanceAdOutcome });
        else
            GameOver();
        }
   private  void SecondChanceAdOutcome(ShowResult result)
        {
        switch (result)
            {
            case ShowResult.Finished:
            SecondChance();
            break;
            case ShowResult.Skipped:
            GameOver();
            break;
            case ShowResult.Failed:
            GameOver();
            break;
            }
        }
    public void SecondChance()
        {
        numberOfGames--;//prevent 2 ads in a row
        Time.timeScale = 1;
        Ball.gameObject.transform.localScale = new Vector3(Ball.gameObject.transform.localScale.x * 0.6f, Ball.gameObject.transform.localScale.y * 0.6f, 0);
        SecondChancePanel.SetActive(false);
        StartCoroutine(nameof(SecondChanceTimer));
        }
    IEnumerator SecondChanceTimer()
        {
        ContinueTimerText.gameObject.SetActive(true);
        for(int i = 3; i > 0; i--)
            {
            ContinueTimerText.text = i.ToString();
            yield return new WaitForSeconds(1);
            }
        ContinueTimerText.gameObject.SetActive(false);
        Ball.GrowSize = Mathf.Clamp(Ball.GrowSize*0.75f, 1.0005f,Ball.GrowSize) ;
        Ball._gameRunning = true;
        Ball.StartCoroutine("BallGrow");
        }
    }
