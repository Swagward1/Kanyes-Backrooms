using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject enemy;

    void Update()
    {
        if(enemy.transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
