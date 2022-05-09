using UnityEngine;
using UnityEngine.UI;

public class FrameCount : MonoBehaviour
{
    int avgFrameRate;
    public Text display_Text;

    public void Update ()
    {
        if(Time.timeScale == 1)
        {
            float current = 0;
            current = Time.frameCount / Time.time;
            avgFrameRate = (int)current;
            display_Text.text = avgFrameRate.ToString() + " FPS";

            //set colour to red
            if(avgFrameRate <= 10)
                display_Text.color = new Color(255, 0, 0, 255);
            //set colour to orange
            else if(avgFrameRate <= 40)
                display_Text.color = new Color(255, 165, 0, 255);
            //set colour to green
            else if(avgFrameRate >= 41)
                display_Text.color = new Color(0, 255, 0, 255);
        }
    }
}