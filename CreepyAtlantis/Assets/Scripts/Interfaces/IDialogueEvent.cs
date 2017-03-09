using UnityEngine;
using System.Collections;

public interface IDialogueEvent {

    void GetMyLines();

    void StartLines();

    void NextLine();
}
