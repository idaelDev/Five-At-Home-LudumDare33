using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WebCreation : MonoBehaviour {

    public bool onWeb = false;
    Web currentWeb;
    SpiderScale sc;
    SpiderEat se;
    float timer;
    public Slider slider;

	// Use this for initialization
	void Start () {
        sc = GetComponent<SpiderScale>();
        se = GetComponent<SpiderEat>();
        slider.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(onWeb)
        {
            if(Input.GetButtonDown("Action"))
            {
                slider.gameObject.SetActive(true);
                timer = 0;
            }
            if(Input.GetButton("Action"))
            {
                timer += Time.deltaTime;
                if(currentWeb.isFeed)
                {
                    slider.maxValue = currentWeb.timeToMake[currentWeb.lvl]/2f;
                    slider.value = timer;
                    if(timer >=  currentWeb.timeToMake[currentWeb.lvl]/2f)
                    {
                        se.Eat(currentWeb.lvl);
                        currentWeb.Eat();
                        timer = 0;
                    }
                }
                else if (sc.level >= currentWeb.lvlRequired[currentWeb.lvl])
                {
                    slider.maxValue = currentWeb.timeToMake[currentWeb.lvl];
                    slider.value = timer;
                    if (timer >= currentWeb.timeToMake[currentWeb.lvl])
                   {
                        currentWeb.MakeWeb();
                        timer = 0;
                    }
                }
                else
                {
                    timer = 0;
                    slider.gameObject.SetActive(false);
                }
            }
            if(Input.GetButtonUp("Action"))
            {
                timer = 0;
                slider.gameObject.SetActive(false);
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
