using UnityEngine;
using System.Collections;

public class Web : MonoBehaviour {

    public delegate void MakingWeb(bool canMove);
    public static event MakingWeb MakingWebEvent;

    public float[] timeToMake;

    public float[] timeToFill;

    public float timeVariation = 0.25f;

    public Animation[] webAnimations;

    public WebShowFood[] webShowFood;

    public int[] lvlRequired;


    public int lvl = 0;
    public bool isFeed = false;

    public void MakeWeb()
    {

        StopAllCoroutines();

        if(lvl>0)
        {
            webAnimations[lvl-1].clip = webAnimations[lvl-1].GetClip("Desappear");
            webAnimations[lvl-1].Play();
        }
        webAnimations[lvl].clip = webAnimations[lvl].GetClip("Appear");
        webAnimations[lvl].Play();
        StartCoroutine(FeedWebCoroutine(lvl));

        lvl++;
    }

    public void Eat()
    {
        isFeed = false;
        for (int i = 0; i < webShowFood.Length; i++)
        {
            webShowFood[i].HideAllBugs();
        }
        StartCoroutine(FeedWebCoroutine(lvl-1));
        //DestroyWeb();
    }

    public void DestroyWeb()
    {
        lvl--;
        StopAllCoroutines();

        if (lvl > 0)
        {
            Debug.Log("Appear");
            webAnimations[lvl - 1].clip = webAnimations[lvl-1].GetClip("Appear");
            webAnimations[lvl - 1].Play();
            StartCoroutine(FeedWebCoroutine(lvl));
        }
        webAnimations[lvl].clip = webAnimations[lvl].GetClip("Desappear");
        webAnimations[lvl].Play();


    }

    IEnumerator MakeToileCoroutine(float time)
    {
        MakingWebEvent(false);
        yield return new WaitForSeconds(time);
        MakingWebEvent(true);
    }

    IEnumerator FeedWebCoroutine(int buflvl)
    {
        float variation = timeToFill[buflvl] * timeVariation;
        float time = timeToFill[buflvl] + ((Random.value * variation * 2) - variation);
        Debug.Log(time + "  :  " + variation);
        yield return new WaitForSeconds(time);
        if (buflvl == 3)
        {
            webShowFood[buflvl].ShowAllBugs();
        }
        else
        {
            webShowFood[buflvl].ShowRandomBug();
        }
        isFeed = true;
    }

}
