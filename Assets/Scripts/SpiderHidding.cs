using UnityEngine;
using System.Collections;

public class SpiderHidding : MonoBehaviour {

    SpiderScale sc;
    public bool isHidding;

    void Start()
    {
        sc = GetComponent<SpiderScale>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hideout0" && sc.level <= 0)
        {
            isHidding = true;
        }
        else if (other.gameObject.tag == "Hideout1" && sc.level <= 1)
        {
            isHidding = true;
        }
        else if (other.gameObject.tag == "Hideout2" && sc.level <= 2)
        {
            isHidding = true;
        }
        else if (other.gameObject.tag == "Hideout3" && sc.level <= 3)
        {
            isHidding = true;
        }
        else if (other.gameObject.tag == "Hideout4" && sc.level <= 4)
        {
            isHidding = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hideout0" && sc.level <= 0)
        {
            isHidding = true;
        }
        else if (other.gameObject.tag == "Hideout1" && sc.level <= 1)
        {
            isHidding = true;
        }
        else if (other.gameObject.tag == "Hideout2" && sc.level <= 2)
        {
            isHidding = true;
        }
        else if (other.gameObject.tag == "Hideout3" && sc.level <= 3)
        {
            isHidding = true;
        }
        else if (other.gameObject.tag == "Hideout4" && sc.level <= 4)
        {
            isHidding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hideout0")
        {
            isHidding = false;
        }
        else if (other.gameObject.tag == "Hideout1")
        {
            isHidding = false;
        }
        else if (other.gameObject.tag == "Hideout2")
        {
            isHidding = false;
        }
        else if (other.gameObject.tag == "Hideout3")
        {
            isHidding = false;
        }
        else if (other.gameObject.tag == "Hideout4")
        {
            isHidding = false;
        }
    }
}
