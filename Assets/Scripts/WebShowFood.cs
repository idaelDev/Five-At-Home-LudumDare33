using UnityEngine;
using System.Collections;

public class WebShowFood : MonoBehaviour {

    public Animation[] bugs;
	
    public void ShowRandomBug()
    {
        int r = Random.Range(0, bugs.Length);
        bugs[r].clip = bugs[r].GetClip("Appear");
        bugs[r].Play();
    }

    public void ShowAllBugs()
    {
        for (int i = 0; i < bugs.Length; i++)
        {
            bugs[i].clip = bugs[i].GetClip("Appear");
            bugs[i].Play();
        }
    }

    public void HideAllBugs()
    {
        for (int i = 0; i < bugs.Length; i++)
        {
            if(bugs[i].GetComponent<SpriteRenderer>().color.a > 0)
            {
                bugs[i].clip = bugs[i].GetClip("Desappear");
                bugs[i].Play();
            }
        }
    }
}
