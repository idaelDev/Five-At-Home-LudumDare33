using UnityEngine;
using System.Collections;

public class SpiderScale : MonoBehaviour {

    public Camera mainCamera;

    public float[] sizeByLevel;
    public float[] cameraSizeByLevel;

    public float timeToAnimate = 0.2f;

    public int level = 0;

    PlayerControl pc;

    void Start()
    {
        pc = GetComponent<PlayerControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        StartCoroutine(SetSizeCoroutine());
    }

    IEnumerator SetSizeCoroutine()
    {
        level++;
        float t = 0;
        Vector3 baseScale = transform.localScale;
        Vector3 targetScale = Vector3.one *  sizeByLevel[level];
        float cameraScale = mainCamera.orthographicSize;

        while(mainCamera.orthographicSize < cameraSizeByLevel[level])
        {
            transform.localScale = Vector3.Lerp(baseScale, targetScale, t / timeToAnimate);
            mainCamera.orthographicSize = Mathf.Lerp(cameraScale, cameraSizeByLevel[level], t / timeToAnimate);
            t += Time.deltaTime;
            yield return 0;
        }
        if(level>0)
        {
            gameObject.GetComponent<SpiderHidding>().isHidding = false;
        }
        if(level == 2 || level == 4)
        {
            pc.UpSpider();
        }
        if(level == 4)
        {
            gameObject.layer = 10;
            Collider2D[] g = GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < g.Length; i++)
            {
                g[i].gameObject.layer = 10;
            }
            pc.isGIant = true;
        }
    }

}
