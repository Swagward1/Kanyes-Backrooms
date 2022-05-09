using UnityEngine;
using UnityEngine.UI;

public class AmmoCounterUI : MonoBehaviour
{
    [SerializeField] Text ammoCounter;
    [SerializeField] GameObject gunObj;
    [SerializeField] Gun gun;

    void Update()
    {
        if (gunObj.activeInHierarchy)
            ammoCounter.text = string.Format("{0}/{1}", gun.currentAmmo, gun.ammoMax);
        else
            ammoCounter.text = string.Empty;
    }
}