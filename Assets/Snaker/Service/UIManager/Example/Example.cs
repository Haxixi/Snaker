using System.Collections;
using System.Collections.Generic;
using Snaker.Service.UIManager;
using UnityEngine;

public class Example
{

    public void Init()
    {
        UIManager.Instance.Init("UI/Example/");
        UIManager.MainPage = "UIPage1";
        UIManager.Instance.EnterMainPage();
    }


}
