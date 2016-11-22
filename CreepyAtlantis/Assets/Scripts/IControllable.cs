using UnityEngine;
using System.Collections;

public interface IControllable {

    void LeftStick(float leftRight, float upDown);

    void RightStick(float leftRight, float upDown);

    void AButton(bool pushRelease, int pNum);

    void LeftBumper(bool pushRelease);

    void RightBumper(bool pushRelease);
}
