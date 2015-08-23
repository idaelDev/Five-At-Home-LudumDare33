using UnityEngine;
using System.Collections;

public class WebCreation : MonoBehaviour {

    bool onWeb = false;
    Web currentWeb;
    SpiderScale sc;
    SpiderEat se;

	// Use this for initialization
	void Start () {
        sc = GetComponent<SpiderScale>();
        se = GetComponent<SpiderEat>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Action"))
        {
            if(onWeb)
            {
                if(currentWeb.isFeed)
                {
                    se.Eat(currentWeb.lvl);
                    currentWeb.isFeed = false;
                    currentWeb.DestroyWeb();
                }
                else if (currentWeb.lvl <= sc.level)
                {
                    currentWeb.MakeWeb();
                }
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Web")
        {

            currentWeb = other.gameObject.GetComponent<Web>();
            onWeb = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Web")
        {
            onWeb = false;
            currentWeb = null;
        }
    }
}
