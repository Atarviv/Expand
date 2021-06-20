using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SecondAnimation : MonoBehaviour
{
    public IEnumerator Fade()
        {
        while (GetComponent<Text>().color != Color.white)
            {
            GetComponent<Text>().color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
            }
        SceneManager.LoadScene("OpenScene");
        }
}
