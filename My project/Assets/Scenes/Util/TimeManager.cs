using UnityEngine;
using System;

// 게임의 속도(2배속, 1배속)와 배터리 절약모드를 담당
public class TimeManager : MonoBehaviour
{
    // 유저가 비활성 상태로 간주되기 전까지의 시간 (초 단위)
    [SerializeField]
    private float idleTimeLimit = 20f;

    // 비활성 상태일 때의 프레임 레이트
    [SerializeField]
    private int reducedFrameRate = 30;

    // 마지막으로 입력이 감지된 시간
    [SerializeField]
    private float lastInputTime;

    // 비활성 상태가 시작된 시간
    private DateTime idleStartTime;

    public bool IsIdle { get; private set; }

    void Start()
    {
        lastInputTime = Time.time;

        // 저장된 값(속도) 불러오기
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

                    // 비활성 상태에서 활성 상태로 전환 시 경과한 시간 계산
                    TimeSpan timeSpan = DateTime.Now - idleStartTime;

                    // 시간에 따른 보상 지급 팝업
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
        // 자동 배터리 절약 모드를 false로 했을 경우 Idle 화면을 표시하지 않음.
        /*if (DataManager.I.optionData.autoPowerSaving == false)
            return;*/

        if (IsIdle == false)
        {
            IsIdle = true;

            // Idle 화면일 때 배터리 절약을 위한 프레임 레이트 조절
            Application.targetFrameRate = reducedFrameRate;

            // (Optional) Idle 화면일 때 속도 조절 
            HalfSpeed();

            // 현재 날짜 및 시간 저장
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
        // 저장된 값으로 속도 변경
        //Time.timeScale = DataManager.I.optionData.isDoubleSpeed ? 2f : 1f;
    }
}