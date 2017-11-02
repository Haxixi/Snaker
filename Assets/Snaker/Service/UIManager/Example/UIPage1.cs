using System.Collections;
using System.Collections.Generic;
using Snaker.Service.UIManager;
using UnityEngine;
using UnityEngine.UI;

public class UIPage1 : UIPage
{
    private Button button;

    protected override void Init()
    {
        button = transform.Find("Button").GetComponent<Button>();

        button.onClick.AddListener(OnBtnOpenPage2);
    }

    protected override void OnOpen(object args = null)
    {
        base.OnOpen(args);
    }

    private void OnBtnOpenPage2()
    {
        UIManager.Instance.OpenPage("UIPage2");
    }
}
