using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    [SerializeField] Camera fpsCam;
    public float range = 100f;


    public void Update()
    {
        if(Input.GetMouseButton(0))
            Shoot();
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            KillEnemy enemy = hit.transform.GetComponent<KillEnemy>();
            
            if(enemy != null)
            {
                enemy.TakeDamage(1);
            }
            
        }
    }
}
