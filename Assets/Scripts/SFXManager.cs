using System.Collections;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource back;
    public AudioSource click;
    public AudioSource toggle;
    public AudioSource jump;

    public AudioSource dreamReveal;
    public AudioSource shutUpDream;
    public AudioSource sketeKanyeConvo1;
    public AudioSource sketeKanyeConvo2;

    #region - Play Sounds -
    public void PlayBack()
    {
        back.Play();
    }

    public void PlayClick()
    {
        click.Play();
    }

    public void PlayToggle()
    {
        toggle.Play();
    }

    public void PlayJump()
    {
        jump.Play();
    }
    #endregion   
    
    #region Lore
    IEnumerator Lore1()
    {
        sketeKanyeConvo1.Play();
        yield return new WaitForSeconds(15f);
        sketeKanyeConvo2.Play();
    }

    IEnumerator Lore2()
    {
        dreamReveal.Play();
        yield return new WaitForSeconds(3.5f);
        shutUpDream.Play();
    }

    #endregion
}