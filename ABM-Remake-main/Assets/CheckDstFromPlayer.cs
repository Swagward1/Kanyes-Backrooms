using UnityEngine;

public class CheckDstFromPlayer : MonoBehaviour
{
    public GameObject player;
    public float validDistFromPlayer;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 distToPlayer = transform.position - player.transform.position;
        //Debug.Log(distToPlayer.magnitude);

        if (distToPlayer.magnitude < validDistFromPlayer)
        {
            anim.SetTrigger("DoorActive");
            Destroy(gameObject, 3f);
        }
    }
}