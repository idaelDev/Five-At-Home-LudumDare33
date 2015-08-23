using UnityEngine;
using System.Collections;

public class WallCaptor : MonoBehaviour {

    public bool isTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Wall")
        {
            isTrigger = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Wall")
        {
            isTrigger = false;
        }
    }
}
