using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableScript : MonoBehaviour
    {
    float Camera_Size, Ball_Scale;
    ExpandingBallScript Ball;
    private void Start()
        {
        Camera_Size = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize;
        Ball_Scale = (Camera_Size - 0.5f) / 3;
        transform.localScale = new Vector3(Ball_Scale, Ball_Scale, 0);
        Ball = GameObject.Find("Maincircle").GetComponent<ExpandingBallScript>();
        }
    private void OnMouseDown()
        {
        transform.localScale = new Vector3(Ball_Scale * 1.1f, Ball_Scale * 1.1f, 0);
        if (Ball != null)
            Ball.ColorPressed(GetComponent<SpriteRenderer>().color);
        }
    private void OnMouseUp() =>
        transform.localScale = new Vector3(Ball_Scale, Ball_Scale, 0);
    }
