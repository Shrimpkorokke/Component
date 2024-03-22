using UnityEngine;

// 카메라 흔들림을 담당
public class CameraShake : MonoBehaviour
{
    // 지속 시간
    [SerializeField]
    private float shakeDuration = 0.5f;

    // 강도
    [SerializeField]
    private float shakeMagnitude = 0.7f;

    // 제동 속도
    [SerializeField]
    private float dampingSpeed = 1.0f;

    private Vector3 initialPosition;
    private float currentShakeDuration;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            currentShakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    // 이 함수를 호출하여 카메라 흔듦
    public void ShakeCamera()
    {
        currentShakeDuration = shakeDuration;
    }
}
