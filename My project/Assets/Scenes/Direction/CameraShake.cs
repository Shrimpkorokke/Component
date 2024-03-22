using UnityEngine;

// ī�޶� ��鸲�� ���
public class CameraShake : MonoBehaviour
{
    // ���� �ð�
    [SerializeField]
    private float shakeDuration = 0.5f;

    // ����
    [SerializeField]
    private float shakeMagnitude = 0.7f;

    // ���� �ӵ�
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

    // �� �Լ��� ȣ���Ͽ� ī�޶� ���
    public void ShakeCamera()
    {
        currentShakeDuration = shakeDuration;
    }
}
