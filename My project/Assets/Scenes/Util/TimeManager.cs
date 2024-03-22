using UnityEngine;
using System;

// ������ �ӵ�(2���, 1���)�� ���͸� �����带 ���
public class TimeManager : MonoBehaviour
{
    // ������ ��Ȱ�� ���·� ���ֵǱ� �������� �ð� (�� ����)
    [SerializeField]
    private float idleTimeLimit = 20f;

    // ��Ȱ�� ������ ���� ������ ����Ʈ
    [SerializeField]
    private int reducedFrameRate = 30;

    // ���������� �Է��� ������ �ð�
    [SerializeField]
    private float lastInputTime;

    // ��Ȱ�� ���°� ���۵� �ð�
    private DateTime idleStartTime;

    public bool IsIdle { get; private set; }

    void Start()
    {
        lastInputTime = Time.time;

        // ����� ��(�ӵ�) �ҷ�����
        /*if (DataManager.I.optionData.isDoubleSpeed)
            DoubleSpeed();

        else
            NormalSpeed();*/
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            lastInputTime = Time.time;

            if (IsIdle == true)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    IsIdle = false;
                    Application.targetFrameRate = -1;
                    ReturntoPreSpeed();

                    // ��Ȱ�� ���¿��� Ȱ�� ���·� ��ȯ �� ����� �ð� ���
                    TimeSpan timeSpan = DateTime.Now - idleStartTime;

                    // �ð��� ���� ���� ���� �˾�
                    /*var gap = timeSpan.TotalSeconds > DefaultTable.StageIdle.GetList().Max_Time ? DefaultTable.StageIdle.GetList().Max_Time : timeSpan.TotalSeconds;
                    GoodsManager.I.CalIdleGoods(gap);
                    var popupReward = PopupManager.I.GetPopup<PopupIdleReward>();
                    popupReward.Open();
                    popupReward.Init();*/
                }
            }
        }

        if (Time.time - lastInputTime > idleTimeLimit)
        {
            AutoSaving();
        }
    }

    public void AutoSaving()
    {
        // �ڵ� ���͸� ���� ��带 false�� ���� ��� Idle ȭ���� ǥ������ ����.
        /*if (DataManager.I.optionData.autoPowerSaving == false)
            return;*/

        if (IsIdle == false)
        {
            IsIdle = true;

            // Idle ȭ���� �� ���͸� ������ ���� ������ ����Ʈ ����
            Application.targetFrameRate = reducedFrameRate;

            // (Optional) Idle ȭ���� �� �ӵ� ���� 
            HalfSpeed();

            // ���� ��¥ �� �ð� ����
            idleStartTime = DateTime.Now;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void NormalSpeed()
    {
        Time.timeScale = 1f;
    }

    public void DoubleSpeed()
    {
        Time.timeScale = 2f;
    }

    public void HalfSpeed()
    {
        Time.timeScale = 0.5f;
    }

    public void ReturntoPreSpeed()
    {
        // ����� ������ �ӵ� ����
        //Time.timeScale = DataManager.I.optionData.isDoubleSpeed ? 2f : 1f;
    }
}