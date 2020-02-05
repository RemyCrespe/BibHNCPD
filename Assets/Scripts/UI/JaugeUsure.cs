
/******************************************************************************************************
 * Redin Laurine
 * 21/01/2020
 * 
 * Script de la Jauge d'usure, a relier a la vie/Usure du personnage
 * La barre d'usure grandis en fonction des dégats, cela fais noircir le robo de l'interface
 *****************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaugeUsure : MonoBehaviour
{
    public Image Usure;

    public int Min;
    public int Max;

    private int mCurrentValue;
    private float mCurrentPercent;

    public void SetHealth(int health)
    {
        if(health != mCurrentValue)
        {
            if(Max - Min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = health;
                mCurrentPercent = (float)mCurrentValue / (float)(Max - Min);
            }
            Usure.fillAmount = mCurrentPercent;
        }
    }
}
