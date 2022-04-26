using UnityEngine;

public class ShowLeroy : MonoBehaviour
{
    [SerializeField] private GameObject leroy;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (leroy.activeInHierarchy)
                leroy.SetActive(false);
            else
                leroy.SetActive(true);
        }
    }

}
