using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingBallScript : MonoBehaviour
    {
    public Color WarningColor;
    bool gotSecondChance,WarningColors;
    public AudioSource source;
    public AudioClip audioClip;
    int PunishmentCounter;
    GameManager Manager;
    public bool _gameRunning,firstframe;
    public List<Color> Colors = new List<Color>();
    Camera MainCamera;
    float ScreenSize;
    public float GrowSize;
    private void Start()
        {
        gotSecondChance = false;
        PunishmentCounter = 0;
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameRunning = true;
        GrowSize = 1.0005f;
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ScreenSize = MainCamera.orthographicSize;
        StartCoroutine(nameof(BallGrow));
        ColorPressed(GetComponent<SpriteRenderer>().color);
        }
    void Update()
        {
        if (transform.localScale.y / 2 >= MainCamera.orthographicSize * 0.7f && !WarningColors&&_gameRunning)
            {
            WarningColors = true;
            StartCoroutine("Warning");
            }
        else if (transform.localScale.y / 2 < MainCamera.orthographicSize * 0.7f)
            WarningColors = false;
        if (transform.localScale.y /2>= MainCamera.orthographicSize)
            {
            MainCamera.backgroundColor = Color.black;
            WarningColors = false;
            if (!gotSecondChance)
                {
                transform.localScale = new Vector3(ScreenSize * 1.9999999f, ScreenSize * 1.9999999f, 0);
                Manager.SecondChanceOption();
                gotSecondChance = true;
                _gameRunning = false;
                }
            else
                Manager.GameOver();
            }
        }
    IEnumerator BallGrow()
        {
        while (_gameRunning)
            {
            if (_gameRunning)
                transform.localScale = new Vector3(transform.localScale.x * GrowSize, transform.localScale.y * GrowSize, 0);
            GrowSize *= 1.00005f;
            yield return new WaitForSeconds(0.05f);
            }
        }
    public Color RandomColor()
        {
        Color color = Colors[Random.Range(0, Colors.Count)];
        return color;
        }
    public void ChangeColor(Color color) =>
        GetComponent<SpriteRenderer>().color = color;
    public void ColorPressed(Color PressedColor)
        {
        if (_gameRunning)
            {
            if (PressedColor == GetComponent<SpriteRenderer>().color)
                {
                transform.localScale = new Vector3(transform.localScale.x / (GrowSize * 1.05f), transform.localScale.y / (GrowSize * 1.05f), 0);
                GrowSize /= 1.00005f;
                ChangeColor(RandomColor());
                Manager.ChangePoints(1);
                if(firstframe)
                source.PlayOneShot(audioClip);
                }
            else
                {
                PunishmentCounter++;
                if (PunishmentCounter == 3)
                    {
                    Manager.ChangePoints(-1);
                    PunishmentCounter = 0;
                    }
                }
            }
        if (!firstframe) firstframe = true;
        }
    IEnumerator Warning()
        {
        while (_gameRunning && WarningColors)
            {
            MainCamera.backgroundColor = WarningColor;
            yield return new WaitForSeconds(0.3f);
            MainCamera.backgroundColor = Color.black;
            yield return new WaitForSeconds(0.3f);
            }
        }
    }
