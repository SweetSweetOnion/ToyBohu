using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float shakeMagnitude = 5f;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private GameObject whiteFlash;

    public void Flash()
    {
        StartCoroutine(WhiteFlash());
    }

    private IEnumerator WhiteFlash()
    {
        whiteFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        whiteFlash.SetActive(false);
    }

    public void Shake()
    {
        StartCoroutine(ShakeUI());
    }

    private IEnumerator ShakeUI()
    {
        float counter = 0f;
        Vector3 positionChangeShake = Vector3.zero;
        for(; ; )
        {
            if (Gamefeel.Instance.IsInFreeze())
            {
                yield return null;
            }
            counter += Time.deltaTime;
            Vector2 change2D = Random.insideUnitCircle * curve.Evaluate(counter / duration) * shakeMagnitude;
            change2D.x = 0f;
            Vector3 change = new Vector3(change2D.x, change2D.y, 0f);
            transform.position += change - positionChangeShake;
            positionChangeShake = change;

            if (counter > duration)
            {
                transform.position -= positionChangeShake;
                break;
            }
            else
            {
                yield return null;
            }
        }
    }
}
