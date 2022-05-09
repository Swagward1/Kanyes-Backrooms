using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] Health hp;
    [SerializeField] CamControl cam;
    [SerializeField] LevelController canvas;
    [SerializeField] GunCollectable gun;
    [SerializeField] Timer time;
    [SerializeField] Rigidbody rb;

    void Update()
    {
        if(hp.health <= 0)
            RunDeath();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("DeathPlane"))
            RunDeath();
    }

    public void RunDeath()
    {
        if (Time.timeScale == 1 || Time.timeScale == .3f)
        {
            Time.timeScale = .5f;

            rb.freezeRotation = false;
            rb.AddForce(transform.forward * -100f, ForceMode.Acceleration);

            if(gun.isActiveAndEnabled)
                gun.Drop();

            canvas.playerUI.SetActive(false);
            canvas.deathUI.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            player.enabled = false;
            cam.enabled = false;

            //time.StopCoroutine("Stopwatch");
        }
    }
}