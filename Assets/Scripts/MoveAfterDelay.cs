using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAfterDelay : MonoBehaviour
{
    public float delay;
    public Vector3 startPos;
    private void Start()
    {
        transform.position = startPos;
        StartCoroutine(MoveChunk(delay));
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
            transform.position = startPos * (1f - scale);
            yield return null;
        }

        //Destroy(gameObject);
        //block.transform.localScale = Vector3.one; // Ensure it ends at the desired scale
    }
}
