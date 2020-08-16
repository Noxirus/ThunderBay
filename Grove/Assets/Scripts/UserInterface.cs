using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gemText = null;
    [SerializeField] TextMeshProUGUI middleText = null;
    [SerializeField] GameObject[] currentHearts = null;
    [SerializeField] GameObject extraHeart = null;
    [SerializeField] Slider energySlider = null;
    float middleTextTimer = 4f;
    bool extraLife = false;

    public void MiddleTextMessage(string message, Color textColor)
    {
        middleText.color = textColor;
        middleText.text = message;
        middleText.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(HideMiddleText());
    }

    IEnumerator HideMiddleText()
    {
        yield return new WaitForSeconds(middleTextTimer);
        middleText.gameObject.SetActive(false);
    }

    public void UpdateHealth(int hearts)
    {
        if(hearts == 4 && extraLife)
        {
            currentHearts[0].SetActive(true);
            currentHearts[1].SetActive(true);
            currentHearts[2].SetActive(true);
            currentHearts[3].SetActive(true);
        }
        
        if(hearts == 3)
        {
            for(int i = 0; i < currentHearts.Length; i++)
            {
                currentHearts[i].SetActive(true);
            }
            if (extraLife)
            {
                currentHearts[3].SetActive(false);
            }
        }
        if(hearts == 2)
        {
            currentHearts[0].SetActive(true);
            currentHearts[1].SetActive(true);
            currentHearts[2].SetActive(false);
            if (extraLife)
            {
                currentHearts[3].SetActive(false);
            }
        }        
        if(hearts == 1)
        {
            currentHearts[0].SetActive(true);
            currentHearts[1].SetActive(false);
            currentHearts[2].SetActive(false);
            if (extraLife)
            {
                currentHearts[3].SetActive(false);
            }
        }        
        if(hearts == 0)
        {
            currentHearts[0].SetActive(false);
            currentHearts[1].SetActive(false);
            currentHearts[2].SetActive(false);
            if (extraLife)
            {
                currentHearts[3].SetActive(false);
            }
        }
    }

    public void SetMaxEnergy(float maxEnergy) {
        energySlider.maxValue = maxEnergy;
        energySlider.value = maxEnergy;
    public void ExtraLife()
    {
        extraHeart.SetActive(true);
        extraLife = true;
    }

    public void UpdateEnergy(float energy)
    {
        energySlider.value = energy;
    }

    public void UpdateGems(int gems)
    {
        gemText.text = "X " + gems;
    }
}
