using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private int _level;
    private static SaveManager _instance;

    public static SaveManager Instance => _instance;

    public int Level
    {
        get => _level;
        set => _level = value;
    }

    private void Awake()
    {
        _level = PlayerPrefs.GetInt("level", 1);
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }

        _instance = this;
    }
}