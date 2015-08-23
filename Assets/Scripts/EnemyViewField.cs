using UnityEngine;
using System.Collections;

public class EnemyViewField : MonoBehaviour {

    public bool seeSpider;
    public Vector3 lastPosition;

	void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!other.gameObject.GetComponent<SpiderHidding>().isHidding)
            {
                if (CheckOcclusion(other.gameObject))
                {
                    seeSpider = true;
                    lastPosition = other.gameObject.transform.position;
                }
                else
                {
                    seeSpider = false;
                }
            }
            else
            {
                seeSpider = false;
            }
        }
        else
        {
            seeSpider = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        seeSpider = false;
    }

    bool CheckOcclusion(GameObject spider)
    {
        Vector3 direction = spider.transform.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if(hit.collider.gameObject.tag == "Player")
        {
            return true;
        }
        return false;
    }
}
