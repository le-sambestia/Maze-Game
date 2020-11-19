using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickdestroy : MonoBehaviour
{
    public bool puzzlePanelUp = true;
    public GameObject puzzlePanel;

    void OnMouseDown()
    {
        puzzlePanelUp = !puzzlePanelUp;
    }
    void Update()
    {
        if (puzzlePanelUp == false)
        {
            puzzlePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

        }
        if (puzzlePanelUp == true)
        {
            puzzlePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
