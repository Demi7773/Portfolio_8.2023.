using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMngr : MonoBehaviour
{
    public GameManager gm;

    public Slider hpSlajd;

    public Text ammoTxt;
    public Slider reloadBar;

    public Text scoreTxt;
    public TextMeshProUGUI endScoreTxt;

    public GameObject dmgFlashScreen;
    public float screenFlashTajmer = 0f;
    public float screenFlashTajmerMax = 0.4f;
    public bool screenRed = false;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        hpSlajd.maxValue = gm.playerMaxHP;
    }
    private void Update()
    {
        hpSlajd.value = gm.playerCurrentHP;
        screenFlashTajmer -= Time.deltaTime;

        if (screenRed && screenFlashTajmer < 0)
        {
            screenRed = false;
            dmgFlashScreen.SetActive(false);
        }
    }

    public void FlashScreenRed()
    {
        screenRed = true;
        screenFlashTajmer = screenFlashTajmerMax;
        dmgFlashScreen.SetActive(true);
    }
}
