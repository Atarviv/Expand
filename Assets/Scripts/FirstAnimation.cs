using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAnimation : MonoBehaviour
{
    public GameObject SGG;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(AnimateStart));        
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator AnimateStart()
        {
        while (transform.localPosition.y > 0.5f|| transform.localPosition.y < -0.5f)
            {
            transform.localPosition += new Vector3(0, -4 * transform.localPosition.y / Mathf.Abs(transform.localPosition.y), 0);
            yield return new WaitForSeconds(0.01f);
            }
        if (SGG != null) SGG.GetComponent<SecondAnimation>().StartCoroutine("Fade");
        }
}
