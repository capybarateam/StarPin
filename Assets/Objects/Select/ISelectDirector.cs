using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectDirector
{
    void BackToTitle();
    Stage GetSelected();
    bool IsSelected(GameObject stage);
    void SetSelected(Stage stage);
    void SetSelected(GameObject stage);
    void StartGame(Stage stage);
}
