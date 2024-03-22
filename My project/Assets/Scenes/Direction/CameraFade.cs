using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ī�޶� Fade In/Out�� ���
public class CameraFade : MonoBehaviour
{
    // ���̵忡 ���� UI �г�
    [SerializeField]
    private Image fadePanel;

    // ���̵� �ξƿ� ������ ���� �ð�
    [SerializeField]
    public float fadeDuration = 1.0f;

    // ���� ȭ�� ���� �ð�
    [SerializeField]
    public float holdDuration = 1.0f; 

    public void StartFade()
    {
        StopCoroutine(FadeSequence());
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // ���̵� �ƿ�
        yield return StartCoroutine(FadeOut());

        // Ȧ��
        yield return new WaitForSeconds(holdDuration);

        // ���̵� ��
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

