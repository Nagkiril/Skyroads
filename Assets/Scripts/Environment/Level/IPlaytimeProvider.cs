using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaytimeProvider
{
    public event Action<float> OnPlaytimeChanged;

    public float GetPlaytime();
}
