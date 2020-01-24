using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaugeUsure : MonoBehaviour
{
    [SerializeField]
    public Image _usure;

    public int P_min;
    public int P_max;

    private int mCurrentValue;
    private float mCurrentPercent;

    private void Start()
    {
        _usure.fillAmount = mCurrentValue;
    }

    public void SetHealth(int health)
    {
        if(health != mCurrentValue)
        {
            if(P_max - P_min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = health;
                mCurrentPercent = mCurrentValue / (P_max - P_min);
            }

            _usure.fillAmount = mCurrentPercent;
        }
    }
}
