using UnityEngine;
using System.Collections;

public class Web : MonoBehaviour {

    public delegate void MakingWeb(bool canMove);
    public static event MakingWeb MakingWebEvent;

    public float timeToMakeLvl1 = 1f;
    public float timeToMakeLvl2 = 2f;
    public float timeToMakeLvl3 = 3f;

    public float timeToFeedLvl1 = 10f;
    public float timeToFeedLvl2 = 20f;
    public float timeToFeedLvl3 = 30f;

    public float timeVariation = 0.25f;

    public Animation lvl1;
    public Animation lvl2;
    public Animation lvl3;

    public WebShowFood wsf1;
    public WebShowFood wsf2;
    public WebShowFood wsf3;

    public int lvl = 0;
    public bool isFeed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void MakeWeb()
    {

        lvl += 2;
        if(lvl == 2)
        {
            StopCoroutine("FeedWebCoroutine");
            StartCoroutine(MakeToileCoroutine(timeToMakeLvl1));
            lvl1.clip = lvl1.GetClip("Appear");
            lvl1.Play();
            StartCoroutine(FeedWebCoroutine(timeToFeedLvl1));
        }
        else if(lvl == 4)
        {
            StopCoroutine("FeedWebCoroutine");
            StartCoroutine(MakeToileCoroutine(timeToMakeLvl2));
            lvl1.clip = lvl1.GetClip("Desappear");
            lvl2.clip = lvl1.GetClip("Appear");
            lvl1.Play();
            lvl2.Play();
            StartCoroutine(FeedWebCoroutine(timeToFeedLvl2));

        }
        else  if (lvl == 6)
        {
            StopCoroutine("FeedWebCoroutine");
            StartCoroutine(MakeToileCoroutine(timeToMakeLvl3));
            lvl2.clip = lvl1.GetClip("Desappear");
            lvl3.clip = lvl1.GetClip("Appear");
            lvl2.Play();
            lvl3.Play();
            StartCoroutine(FeedWebCoroutine(timeToFeedLvl3));
        }
    }

    public void DestroyWeb()
    {
        wsf1.HideAllBugs();
        wsf2.HideAllBugs();
        wsf3.HideAllBugs();
        lvl -= 2;
        if (lvl == 0)
        {
            StopCoroutine("FeedWebCoroutine");
            lvl += 2;
            StartCoroutine(FeedWebCoroutine(timeToFeedLvl1));
        }
        else if (lvl == 2)
        {
            StopCoroutine("FeedWebCoroutine");
            lvl1.clip = lvl1.GetClip("Appear");
            lvl2.clip = lvl2.GetClip("Desappear");
            lvl1.Play();
            lvl2.Play();
            StartCoroutine(FeedWebCoroutine(timeToFeedLvl2));

        }
        else if (lvl == 4)
        {
            StopCoroutine("FeedWebCoroutine");
            lvl2.clip = lvl2.GetClip("Appear");
            lvl3.clip = lvl3.GetClip("Desappear");
            lvl2.Play();
            lvl3.Play();
            StartCoroutine(FeedWebCoroutine(timeToFeedLvl3));
        }
    }

    IEnumerator MakeToileCoroutine(float time)
    {
        MakingWebEvent(false);
        yield return new WaitForSeconds(time);
        MakingWebEvent(true);
    }

    IEnumerator FeedWebCoroutine(float baseTime)
    {
        float variation = baseTime * timeVariation;
        float time = baseTime + ((Random.value * variation * 2) - variation);
        Debug.Log(time + "  :  " + variation);
        yield return new WaitForSeconds(time);
        if(lvl == 6)
        {
            wsf3.ShowAllBugs();
        }
        else if(lvl == 4)
        {
            wsf2.ShowRandomBug();
        }
        else if(lvl == 2)
        {
            wsf1.ShowRandomBug();
        }
        isFeed = true;
    }

}
