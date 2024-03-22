using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ContinuousButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonPressed = false;

    // �ʱ� ������
    [SerializeField]
    private float initialDelay = 0.5f;

    // ������ ���ҷ�
    [SerializeField]
    private float delayDecrement = 0.05f;

    // ���� ������
    private float currentDelay;
    
    private Action buttonAction;
    private void Start()
    {
        currentDelay = initialDelay;
    }

    private IEnumerator ButtonPressedRoutine()
    {
        while (isButtonPressed)
        {
            // ���⿡ ��ư�� ������ �� ȣ���� �Լ��� �ֽ��ϴ�.
            ButtonFunction(); 
            yield return new WaitForSeconds(currentDelay);

            // ������ ���� (�ּҰ� ����)
            if (currentDelay > 0.05f)
            {
                currentDelay -= delayDecrement;
            }
        }
    }

    // �ܺο��� ȣ���� �Լ� ����
    public void SetButtonAction(Action newAction)
    {
        buttonAction = newAction;
    }

    // ��ư�� ������ �� ȣ��� �Լ�
    private void ButtonFunction()
    {
        buttonAction?.Invoke();
    }

    // IPointerDownHandler �������̽� ����
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isButtonPressed)
        {
            isButtonPressed = true;
            currentDelay = initialDelay;
            StartCoroutine(ButtonPressedRoutine());
        }
    }

    // IPointerUpHandler �������̽� ����
    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }
}
