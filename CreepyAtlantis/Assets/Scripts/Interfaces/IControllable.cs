using UnityEngine;
using System.Collections;

public interface IControllable {

    void LeftStick(float leftRight, float upDown, int pNum);

    void RightStick(float leftRight, float upDown, int pNum);

    void AButton(bool pushRelease, int pNum);

    void YButton(bool pushRelease, int pNum);

    void LeftBumper(bool pushRelease, int pNum);

    void RightBumper(bool pushRelease, int pNum);
}
