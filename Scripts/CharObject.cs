using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharObject : MonoBehaviour
{
    public char character; //karakter abjad
    public Text text; //referensi untuk text pada unity
    public Image image;
    public RectTransform rectTransfrom;
    public int index; //index dari char

    public Color normalColor;
    public Color selectedColor;
    bool isSelected = false;

    public CharObject Init(char c)
    {
        character = c;
        text.text = c.ToString();
        gameObject.SetActive(true);
        return this;
    }

    public void Select ()
    {
        isSelected = !isSelected;

        image.color = isSelected ? selectedColor : normalColor;
        if (isSelected)
        {
            WordScramble.main.Select(this);
        }else
        {
            WordScramble.main.UnSelect();
        }
    }


}
