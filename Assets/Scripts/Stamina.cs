using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField]int health;
    [SerializeField]int numOfHearts;

    [SerializeField]Image[] hearts;
    [SerializeField]Sprite fullHeart;
    [SerializeField]Sprite emptyHeart;

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(health > numOfHearts)
                health = numOfHearts;

            if(i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;


            if(i < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
}
