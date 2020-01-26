using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ValueChange : MonoBehaviour
{
            //VALUES//
    [Header("Základní Hodnoty")]
    //Max HP Variable
    [SerializeField]
    public float MaxValue = 50f;

    //Based On Current HP
    [SerializeField]
    public float CurrentValue = 50f;

    private float MaxValueCheck;

            //IMAGES//
    [Space]
    [Header("ValueBar")]
    //Reference To BaseImage Component;
    public Image BaseImage;
    //Reference To LerpImage Component
    public Image ValueImage;

    //BOOL//
    //Bool For Allow Lerping
    public bool ValueLerpDown;
    //Bool For Allow Lerping
    public bool ValueLerpUp;
    //Bool For Allow Lerping
    public bool MaxValueLerp;

    //SETTINGS//
    [Space]
    [Header("Čas na přechod v sekundách")]
    [Range(0.2f, 2f)]
    [SerializeField]
    private float LerpingTime = 0.5f;

    [Range(0.1f, 1f)]
    [SerializeField]
    private float BaseLerpingTime = 0.25f;

    //PROPERTIES//
    [Space]
    //Time Variable
    private float waitTime;
    private float GreenwaitTime;
    private float ValuewaitTime;

    //Amount To Lerp to
    private float EndFillAmount;

    //Get Current fillAmount Of Image Component
    private float StartFillAmount;

    //Equal To Base float Of (waitTime)
    private float MaxProgress;

    //Current Progress Of RedBar Lerping
    private float Progress;

    //Current Progress Of GreenBar Lerping
    private float GreenProgress;

    private float ValueProgress;

    public Text text;

    //Turning On/Off Testing Healing
    [Tooltip("Zapnutí Testovací Regenerace")]
    public bool DebugHealthRegen = false;









    void Start()
    {
        MaxValueCheck = MaxValue;
        CurrentValue = MaxValue;
        StartFillAmount = (ValueImage.fillAmount);
        EndFillAmount = CurrentValue / MaxValue;
        GameObject ValueText = GameObject.Find("ValueText");
        text = ValueText.GetComponent<Text>();
    }













    // Update is called once per frame
    void Update()
    {
        //Text Component Values - each frame
        text.text = CurrentValue + "/" + MaxValue;

        if (Input.GetKeyDown(KeyCode.D))
        {
            MaxValue -= 25f;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            MaxValue += 25f;
        }

        //DEBUG LERP DOWN
        if (Input.GetKeyDown(KeyCode.E))
        {
            CurrentValue -= 5f;
            StartFillAmount = (ValueImage.fillAmount);
            EndFillAmount = CurrentValue / MaxValue;
            waitTime = LerpingTime;
            GreenwaitTime = BaseLerpingTime;
            MaxProgress = LerpingTime;
            ValueLerpDown = true;
        }

        if (ValueLerpDown == true)

        {
            Progress = (waitTime - Time.deltaTime) / (MaxProgress);
            waitTime = waitTime - Time.deltaTime;
            ValueImage.fillAmount = Mathf.Lerp(EndFillAmount, StartFillAmount, Progress);

            GreenProgress = (GreenwaitTime - Time.deltaTime) / (MaxProgress);
            GreenwaitTime = GreenwaitTime - Time.deltaTime;
            BaseImage.fillAmount = Mathf.Lerp(EndFillAmount, StartFillAmount, GreenProgress);

            if (waitTime >= 1 && GreenProgress >= 1)
            {
                ValueLerpDown = false;
            }
        }
        //END

        //DEBUG LERP UP
        if (Input.GetKeyDown(KeyCode.W))
        {
            CurrentValue += 5f;
            StartFillAmount = (ValueImage.fillAmount);
            EndFillAmount = CurrentValue / MaxValue;
            waitTime = BaseLerpingTime;
            GreenwaitTime = LerpingTime;
            MaxProgress = LerpingTime;
            ValueLerpUp = true;
        }

        if (ValueLerpUp == true)

        {
            Progress = ((waitTime - Time.deltaTime) / (-MaxProgress));
            waitTime = waitTime - Time.deltaTime;
            ValueImage.fillAmount = Mathf.Lerp(StartFillAmount, EndFillAmount, Progress);

            GreenProgress = ((GreenwaitTime - Time.deltaTime) / (-MaxProgress));
            GreenwaitTime = GreenwaitTime - Time.deltaTime;
            BaseImage.fillAmount = Mathf.Lerp(StartFillAmount, EndFillAmount, GreenProgress);

            if (Progress >= 1 && GreenProgress >= 1)
            {
                ValueLerpUp = false;
            }
        }

        
        //END

        //IF MAXVALUE CHANGE -> Execute "Function"
        if (MaxValueCheck != MaxValue)
        {
            MaxValueCheck = MaxValue;
            ValuewaitTime = LerpingTime;
            MaxProgress = LerpingTime;
            StartFillAmount = (ValueImage.fillAmount);
            EndFillAmount = CurrentValue / MaxValue;
            MaxValueLerp = true;
        }
        //END

        //"FUNCTION"
        if (MaxValueLerp == true)
        {
            ValueProgress = (ValuewaitTime - Time.deltaTime) / (MaxProgress);
            ValuewaitTime = ValuewaitTime - Time.deltaTime;
            BaseImage.fillAmount = Mathf.Lerp(EndFillAmount, StartFillAmount, ValueProgress);
            ValueImage.fillAmount = Mathf.Lerp(EndFillAmount, StartFillAmount, ValueProgress);
        }
        //END

        //STOP CONDITION
        if (ValueProgress <= 0)
        {
            MaxValueLerp = false;
        }
        //END


        //DEBUG HEALTH REGEN
        if (waitTime <= 0)
        {
            ValueLerpDown = false;
        }

        if (ValueLerpDown == false && CurrentValue <= MaxValue && DebugHealthRegen == true && ValueLerpUp == false)
        {
            CurrentValue += (0.01f + (Time.deltaTime / 100));
        }
        //END OF DEBUG HEALTH REGEN

        //CLAMP VALUE (Nelze jít nad maximální hodnotu ani pod 0!)
        if (CurrentValue > MaxValue)
        {
            CurrentValue = MaxValue;
        }

        if (CurrentValue < 0f)
        {
            CurrentValue = 0f;
        }
        //END

    }
}