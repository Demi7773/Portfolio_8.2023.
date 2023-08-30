using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldInput : MonoBehaviour
{
    [SerializeField] Text fieldTxt;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnClick()
    {
        fieldTxt.text = GameManager.instance.playerSide;
        button.interactable = false;
        GameManager.instance.turnCount++;
        GameManager.instance.WinCheck();
    }
}
