using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUI : MonoBehaviour
{
    public void UIEnable()
    {
        GameObject.Find("Canvas/Menu/UI").SetActive(true);
    }
}
