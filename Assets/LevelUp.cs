﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    [SerializeField]
    private int CurrentLevel = 1;
    [SerializeField]
    private float CurrentExperience = 0;
    [SerializeField]
    private float NeededExperience = 2500;
    [SerializeField]
    private int SkillPoints = 0;

    public float[] values;
        
    private Text text;

    //private Image ExperienceImage;







    // Start is called before the first frame update
    void Start()
    {
        GameObject ExperienceText = GameObject.Find("ExperienceText");
        text = ExperienceText.GetComponent<Text>();
        values = new float[10];
        values[0] = 2500;
        values[1] = 3000;
        values[2] = 4500;
        values[3] = 6000;
        values[4] = 7500;
        values[5] = 9000;
        values[6] = 11500;
        values[7] = 13000;
        values[8] = 14500;
        values[9] = 16000;
        //TODO!!!!//
        //GameObject ExperienceImage = GameObject.Find("ExperienceImage");
        //ValueChange valueChange = ExperienceImage.GetComponent<ValueChange>();
        //valueChange.CurrentValue = CurrentExperience;
        //valueChange.MaxValue = NeededExperience;
        //valueChange.ValueLerpUp = true;
    }






    // Update is called once per frame
    void Update()
    {

        text.text = CurrentExperience + "/" + NeededExperience;

        if (Input.GetKeyDown(KeyCode.C))
        {
            CurrentExperience += 1255;
            
        }

        if (CurrentExperience >= NeededExperience)
        {
            OnLevelUp();
            CurrentExperience = 0 + CurrentExperience - NeededExperience;
            NeededExperience = values[CurrentLevel - 1];
        }
    }

    void OnLevelUp()
    {
        CurrentLevel += 1;
        SkillPoints += 1;
        if (CurrentLevel > 9)
        {
            SkillPoints += 1;
        }
    }
}
