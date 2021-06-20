using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SignSwitcher : MonoBehaviour
{
    public Sprite redSign, greenSign, btnUnpressed, btnPressed;
    float timeToSwitch = 3f, reduceFactor = 1.1f;
    public Image signImage, slickBImage;
    public Button blueB;
    bool buttonPressed;

    // Start is called before the first frame update
    void Start()
    {
        blueB.onClick.AddListener(() => { buttonPressed = true; slickBImage.sprite = btnPressed; });
    }

    // Update is called once per frame
    void Update()
    {
        if (signImage.sprite == redSign && buttonPressed) {
            blueB.enabled = false; 
            enabled = false;
        }

        if (timeToSwitch > 0f) timeToSwitch -= Time.deltaTime;
        else
        {
            if (signImage.sprite == greenSign) {
                signImage.sprite = redSign;
                if (buttonPressed) {
                    buttonPressed = false;
                    slickBImage.sprite = btnUnpressed;
                } else {
                    blueB.enabled = false;
                    enabled = false;
                }
            }
            else if (!buttonPressed) signImage.sprite = greenSign;

            timeToSwitch = 3f / reduceFactor;
            reduceFactor += 0.1f;
        }
    }
}
