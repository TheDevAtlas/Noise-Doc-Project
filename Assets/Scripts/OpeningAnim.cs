using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningAnim : MonoBehaviour
{
    public Transform pivotPoint;
    public Camera cam;
    public Vector3 startPos;

    private void Start()
    {
        transform.position = startPos;
        StartCoroutine(MoveChunk(0f));
        StartCoroutine(MoveCamera(2f));
    }

    private float EaseInOut(float current, float total)
    {
        float x = current / total;
        return x < 0.5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    IEnumerator MoveChunk(float moveDelay)
    {
        yield return new WaitForSeconds(moveDelay);

        float time = 0;
        float duration = 3f; // Duration of the scale animation

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = EaseInOut(time, duration);
            transform.position = startPos * (1f-scale);
            yield return null;
        }

        //Destroy(gameObject);
        //block.transform.localScale = Vector3.one; // Ensure it ends at the desired scale
    }

    IEnumerator MoveCamera(float moveDelay)
    {
        yield return new WaitForSeconds(moveDelay);

        float time = 0;
        float duration = 1.2f; // Duration of the scale animation

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = EaseInOut(time, duration);
            //transform.position = startPos * (1f - scale);

            cam.orthographicSize = Mathf.Lerp(12.86f, 18f, scale);
            pivotPoint.localEulerAngles = Vector3.Lerp(new Vector3(20,45,0), new Vector3(30,45,0),scale);
            yield return null;
        }
        
    }
}
