using UnityEngine;
using System.Collections;

public class SpiderEat : MonoBehaviour {

    public int[] valueToNextLevel;
    public int[] foodValue;
    public int currentFoodValue = 0;
    public float eatTime = 0.5f;

    private SpiderScale sc;
    private PlayerControl pc;
    SoundSpiderManager audio;

	// Use this for initialization
	void Start () {
        sc = GetComponent<SpiderScale>();
        pc = GetComponent<PlayerControl>();
        audio = GetComponent<SoundSpiderManager>();
	}
	
    public void Eat(int lvl)
    {
        pc.anim.SetTrigger("Eat");
        audio.PlayEat();
        StartCoroutine(EatCoroutine());
        currentFoodValue += foodValue[lvl];
        if(currentFoodValue >= valueToNextLevel[sc.level])
        {
            LevelUp();
        }
    }

    IEnumerator EatCoroutine()
    {
        pc.SetCanMove(false);
        yield return new WaitForSeconds(eatTime);
        pc.SetCanMove(true);
    }

    void LevelUp()
    {
        sc.LevelUp();
    }
}
