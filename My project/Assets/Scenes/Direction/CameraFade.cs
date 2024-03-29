using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 카메라 Fade In/Out을 담당
public class CameraFade : MonoBehaviour
{
    // 페이드에 사용될 UI 패널
    [SerializeField]
    private Image fadePanel;

    // 페이드 인아웃 각각의 지속 시간
    [SerializeField]
    public float fadeDuration = 1.0f;

    // 검은 화면 유지 시간
    [SerializeField]
    public float holdDuration = 1.0f; 

    public void StartFade()
    {
        StopCoroutine(FadeSequence());
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // 페이드 아웃
        yield return StartCoroutine(FadeOut());

        // 홀드
        yield return new WaitForSeconds(holdDuration);

        // 페이드 인
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, alpha);
            yield return null;
        }
    }
}

