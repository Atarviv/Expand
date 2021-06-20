using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenSceneManager : MonoBehaviour
{
    public List<Text> HighScores = new List<Text>();
    public List<Button> Buttons = new List<Button>();
    public Color color;
    ColorBlock ChosenColor, DefaultColor;
    public static int level;
    // Start is called before the first frame update
    void Start()
    {
        DefaultColor = Buttons[0].colors;
        ChosenColor = DefaultColor;
        ChosenColor.normalColor = color;
        ChosenColor.selectedColor = color;
        ChangeLevel(0);
        for(int i = 0; i < HighScores.Count; i++)
            {
            HighScores[i].text +=" "+ PlayerPrefs.GetInt("HighScore" + i).ToString();
            }
    }
    public void ChangeLevel(int Level)
        {
        level = Level;
        for (int i = 0; i < Buttons.Count; i++)
            {
            if (i == Level)
                Buttons[i].colors = ChosenColor;
            else
                Buttons[i].colors = DefaultColor;
            }
        }
    public void StartGame() =>
        SceneManager.LoadScene("SampleScene");
}
