﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int points = 0;
    private PlayerMovementStreet pms;

    public static Counter instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }

        instance = this;

        PlayerPrefs.DeleteAll();
        pms = FindObjectOfType<PlayerMovementStreet>();
    }



    
    public void AddPoints(int ID)
    {
        points++;
        pms.speed = 5f / (float)points;
        PlayerPrefs.SetInt($"Has_PNJ_{ID}_Been_Saved", 1);

        if(points == 5)
        {
            pms.flamme.SetActive(true);
        }
    }

    public void GetEndingBasedOnPointsLeft()
    {
        PlayerPrefs.SetInt("Nobody", points == 0 ? 1 : 0);
        PlayerPrefs.SetInt("Everybody", points == 5 ? 1 : 0);
    }
}
