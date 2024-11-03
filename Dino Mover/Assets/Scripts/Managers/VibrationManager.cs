using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class VibrationManager : DemoManager
{
    

    public void LowVibration()
    {

    }

    public void MediumVibration()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
    }
    public void HighVibration()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact, false, true, this);
    }

    public void FailureVibration()
    {
        MMVibrationManager.Haptic(HapticTypes.Failure, false, true, this);
    }
}
