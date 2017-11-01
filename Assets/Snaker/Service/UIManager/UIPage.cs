using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snaker.Service.UIManager
{

    public class UIPage : UIPanel
    {

        [SerializeField] private Button m_btnGoBack;

        protected object m_openArgs;

        private bool m_isOpenedOnce;

        protected void OnEnable()
        {
            Debug.Log(this.GetType().Name + "  OnEnable  ");

            if (m_btnGoBack != null)
                m_btnGoBack.onClick.AddListener(OnBtnGoBack);

#if UNITY_EDITOR
            if (m_isOpenedOnce)
                OnOpen(m_openArgs);
#endif
        }

        protected void OnDisable() 
        {
            Debug.Log(this.GetType().Name + "  OnDisable  ");

#if UNITY_EDITOR
            if (m_isOpenedOnce)
                OnClose();
#endif

            if (m_btnGoBack != null)
                m_btnGoBack.onClick.RemoveAllListeners();
        }

        private void OnBtnGoBack()
        {
            Debug.Log(this.GetType().Name + "  OnBtnGoBack  ");
            UIManager.Instance.GoBackPage();
        }

        public sealed override void Open(object args = null)
        {
            Debug.Log(this.GetType().Name + "  Override Open  ");
            m_openArgs = args;
            m_isOpenedOnce = false;

            if(!this.gameObject.activeSelf)
                this.gameObject.SetActive(true);

            OnOpen(args);
            m_isOpenedOnce = true;
        }

        public sealed override void Close()
        {
            Debug.Log(this.GetType().Name + "  Override Close  ");
            if(this.gameObject.activeSelf)
                this.gameObject.SetActive(false);

            OnClose();
        }
    }
}


