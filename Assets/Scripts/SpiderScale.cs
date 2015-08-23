using UnityEngine;
using System.Collections;

public class SpiderScale : MonoBehaviour {

    public Camera mainCamera;

    public float[] sizeByLevel;
    public float[] cameraSizeByLevel;

    public float timeToAnimate = 0.2f;

    public int level = 0;

    public void LevelUp()
    {
        StartCoroutine(SetSizeCoroutine());
    }

    IEnumerator SetSizeCoroutine()
    {
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
        level++;
    }

}
