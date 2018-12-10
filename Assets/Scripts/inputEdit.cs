using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inputEdit : MonoBehaviour {

    // Hides the keyboard if the device is facing down
    // and resumes input if the device is facing up.

    TouchScreenKeyboard keyboard;

    void Update()
    {
        if (keyboard != null)
        {
            if (Input.deviceOrientation == DeviceOrientation.FaceDown)
                keyboard.active = false;
            if (Input.deviceOrientation == DeviceOrientation.FaceUp)
                keyboard.active = true;
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 10, 200, 32), "Open keyboard"))
            keyboard = TouchScreenKeyboard.Open("text");
    }
}
