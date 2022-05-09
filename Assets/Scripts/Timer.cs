using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;
    public float time;

    float milliseconds;
    float seconds;
    float minutes;

    void Start()
    {
        Time.timeScale = 1;
    }

    IEnumerator Stopwatch()
    {
        while (true)
        {
            time += Time.deltaTime;
            milliseconds = (int)((time - (int)time) * 100);
            seconds = (int)(time % 60);
            minutes = (int)(time / 60 % 60);

            timer.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);

            yield return null;
        }
    }
}