using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScaleCube(gameObject));
    }

    IEnumerator ScaleCube(GameObject block)
    {
        float time = 0;
        float duration = 1f; // Duration of the scale animation

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = EaseInOut(time, duration);
            block.transform.localScale = new Vector3(scale, scale, scale) * 17f;
            yield return null;
        }

        block.transform.localScale = Vector3.one * 17f; // Ensure it ends at the desired scale
    }

    private float EaseInOut(float current, float total)
    {
        float x = current / total;
        return x < 0.5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }
}
