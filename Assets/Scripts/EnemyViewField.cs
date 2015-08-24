using UnityEngine;
using System.Collections;

public class EnemyViewField : MonoBehaviour {

    public bool seeSpider;
    public Vector3 lastPosition;

    public delegate void SeeSpider();
    public event SeeSpider SeeSpiderEvent;

	void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!other.gameObject.GetComponent<SpiderHidding>().isHidding)
            {
                if (CheckOcclusion(other.gameObject))
                {
                    Debug.Log("See");
                    seeSpider = true;
                    lastPosition = other.gameObject.transform.position;
                    SeeSpiderEvent();
                }
                else
                {
                    Debug.Log("Un See");
                    seeSpider = false;
                }
            }
            else
            {
                Debug.Log("Un See");
                seeSpider = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Un See");
        seeSpider = false;
    }

    bool CheckOcclusion(GameObject spider)
    {
        Vector3 direction = spider.transform.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        //Debug.Log(hit.collider.gameObject.name);
        if(hit.collider.gameObject.tag == "Player")
        {
            return true;
        }
        return false;
    }
}
