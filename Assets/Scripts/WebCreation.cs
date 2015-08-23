using UnityEngine;
using System.Collections;

public class WebCreation : MonoBehaviour {

    bool onWeb = false;
    Web currentWeb;
    SpiderScale sc;
    SpiderEat se;
    float timer;

	// Use this for initialization
	void Start () {
        sc = GetComponent<SpiderScale>();
        se = GetComponent<SpiderEat>();
	}
	
	// Update is called once per frame
	void Update () {
        if(onWeb)
        {
            if(Input.GetButtonDown("Action"))
            {
                
                timer = 0;
            }
            if(Input.GetButton("Action"))
            {
                timer += Time.deltaTime;
                if(currentWeb.isFeed)
                {
                    if(timer >=  currentWeb.timeToMake[currentWeb.lvl])
                    {
                        se.Eat(currentWeb.lvl);
                        currentWeb.Eat();
                        timer = 0;
                    }
                }
                else if (sc.level >= currentWeb.lvlRequired[currentWeb.lvl])
                {
                    if (timer >= currentWeb.timeToMake[currentWeb.lvl])
                   {
                        currentWeb.MakeWeb();
                        timer = 0;
                    }
                }
                else
                {
                    timer = 0;
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
