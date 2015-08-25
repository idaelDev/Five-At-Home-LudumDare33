using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpiderEat : MonoBehaviour {

    public int[] valueToNextLevel;
    public int[] foodValue;
    public int currentFoodValue = 0;
    public float eatTime = 0.5f;
    public Slider slider;
    public Text levelText;
    private SpiderScale sc;
    private PlayerControl pc;
    SoundSpiderManager audio;

    public string[] lvlMessages;

	// Use this for initialization

	void Start () {
        sc = GetComponent<SpiderScale>();
        pc = GetComponent<PlayerControl>();
        audio = GetComponent<SoundSpiderManager>();
        slider.minValue = currentFoodValue;
        slider.maxValue = valueToNextLevel[sc.level];
	}
	
    public void Eat(int lvl)
    {
        pc.anim.SetTrigger("Eat");
        audio.PlayEat();
        StartCoroutine(EatCoroutine());
        currentFoodValue += foodValue[lvl];
        slider.value = currentFoodValue;

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
        slider.minValue = currentFoodValue;
        slider.maxValue = valueToNextLevel[sc.level + 1];
        levelText.text = lvlMessages[sc.level+1];
        sc.LevelUp();
    }
}
