using UnityEngine;

public class ObjectPickUp : MonoBehaviour
{
    public float pickUpRange = 5f;
    public float MoveObjectForce = 250f;
    public Transform holdParent;
    private GameObject heldObject;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(heldObject == null)
            {
                RaycastHit hitInfo;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, pickUpRange))
                {
                    PickUpObject(hitInfo.transform.gameObject);
                }
            }

            else
            {
                DropHeldObject();
            }
        }

        if(heldObject != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObject.transform.position, holdParent.position) > .1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * MoveObjectForce);
        }
    }

    void PickUpObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRB = pickObj.GetComponent<Rigidbody>();

            objRB.useGravity = false;
            objRB.drag = 10;
            objRB.transform.parent = holdParent;

            heldObject = pickObj;
        }
    }

    void DropHeldObject()
    {
        Rigidbody heldRig = heldObject.GetComponent<Rigidbody>();

        heldRig.useGravity = true;
        heldRig.drag = 1;

        heldObject.transform.parent = null;
        heldObject = null;
    }
}
