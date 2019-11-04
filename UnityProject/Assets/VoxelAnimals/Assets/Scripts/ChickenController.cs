using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ChickenController : MonoBehaviour
{
    Animator anim;
    public Rigidbody rb;

    string objectName;

    private float zMax = 0.5f;
    private float zMin = -0.5f;
    private float z = 0.01f;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ControllPlayer();
    }

    void ControllPlayer()
    {
        rb.GetComponent<Collider>().enabled = true;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                objectName = hit.transform.name;
                switch (objectName)
                {
                    case "Chicken":
                        anim.SetInteger("Walk", 0);
                        anim.SetTrigger("jump");
                        rb.AddForce(0, 2, 0, ForceMode.Impulse);
                        break;
                    default:
                        Debug.Log("This isn't a Chicken");
                        break;
                }
            }
        }

        anim.SetInteger("Walk", 1);

        if (transform.localPosition.z >= zMax)
        {
            transform.Rotate(0, 180, 0);
            z = -0.01f;
        }

        if (transform.localPosition.z <= zMin)
        {
            transform.Rotate(0, 180, 0);
            z = 0.01f;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + z);

    }
}