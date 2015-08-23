using UnityEngine;
using System.Collections;

public class SpiderEat : MonoBehaviour {

    public int[] valueToNextLevel;
    public int currentFoodValue = 0;
    public float eatTime = 0.5f;

    private SpiderScale sc;
    private PlayerControl pc;

	// Use this for initialization
	void Start () {
        sc = GetComponent<SpiderScale>();
        pc = GetComponent<PlayerControl>();
	}
	
    public void Eat(int foodValue)
    {
        pc.anim.SetTrigger("Eat");
        StartCoroutine(EatCoroutine());
        currentFoodValue += foodValue;
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
