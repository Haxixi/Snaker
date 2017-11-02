using System.Collections;
using System.Collections.Generic;
using Snaker.Service.UIManager;
using UnityEngine;
using UnityEngine.UI;

public class UIPage2 : UIPage
{
    private Button Button1;
    private Button Button2;

    protected override void Init()
    {
        Button1 = transform.Find("Button1").GetComponent<Button>();
        Button2 = transform.Find("Button2").GetComponent<Button>();
        Button1.onClick.AddListener(OnBtnOpenWnd1);
        Button2.onClick.AddListener(OnBtnOpenWidget1);
    }


    private void OnBtnOpenWnd1()
    {
        UIManager.Instance.OpenWindow<UIWnd1>().onClose += OnWnd1Close;
    }


    private void OnWnd1Close()
    {
        Debug.Log(this.GetType().Name+"  OnWnd1CloseEvent");
    }


    private void OnBtnOpenWidget1()
    {
        UIManager.Instance.OpenWidget("UIWidget1");
    }
}
