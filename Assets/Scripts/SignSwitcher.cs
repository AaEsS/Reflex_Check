using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class SignSwitcher : MonoBehaviour
{
    public Sprite redSign, greenSign, btnUnpressed, btnPressed;
    float timeToSwitch = 3f, reduceFactor = 1.1f;
    public Image signImage, slickBImage;
    bool buttonPressed, pressedOnce;
    AudioSource soundPlayer;
    public AudioClip buttonSound, signSound, gameOverSound;
    float result = 0f;
    public TextMeshProUGUI slickResult, bestResult;
    public GameObject deathP;

    // Start is called before the first frame update
    void Start()
    {
        bestResult.SetText($"Best result: {PlayerPrefs.GetFloat("BestSlickness", 0f)}% slick");
        soundPlayer = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !pressedOnce)
        {
            buttonPressed = true;
            slickBImage.sprite = btnPressed;
            soundPlayer.PlayOneShot(buttonSound);
            pressedOnce = true;
        }

        if (signImage.sprite == redSign && buttonPressed) {
            soundPlayer.PlayOneShot(gameOverSound);
            deathP.SetActive(true);
            enabled = false;
        }

        if (timeToSwitch > 0f) timeToSwitch -= Time.deltaTime;
        else
        {
            if (signImage.sprite == greenSign)
            {
                signImage.sprite = redSign;
                pressedOnce = false;
                if (buttonPressed)
                {
                    buttonPressed = false;
                    slickBImage.sprite = btnUnpressed;
                    result = (float)Math.Round(reduceFactor * 2, 2);
                    slickResult.SetText($"You are {result}% slick");
                    if (PlayerPrefs.GetFloat("BestSlickness") < result) PlayerPrefs.SetFloat("BestSlickness", result);
                    bestResult.SetText($"Best result: {PlayerPrefs.GetFloat("BestSlickness")}% slick");
                }
                else
                {
                    soundPlayer.PlayOneShot(gameOverSound);
                    deathP.SetActive(true);
                    enabled = false;
                }
            }
            else if (!buttonPressed) { 
                signImage.sprite = greenSign;
                soundPlayer.PlayOneShot(signSound);
            }

            timeToSwitch = 3f / reduceFactor;
            reduceFactor += 0.1f;
        }
    }
}
