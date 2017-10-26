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
            Debug.Log("OnEnable  ");

            if (m_btnGoBack != null)
                m_btnGoBack.onClick.AddListener(OnBtnGoBack);

#if UNITY_EDITOR
            if (m_isOpenedOnce)
                OnOpen(m_openArgs);
#endif
        }

        protected void OnDisable()
        {
            Debug.Log("OnDisable  ");

#if UNITY_EDITOR
            if (m_isOpenedOnce)
                OnClose();
#endif

            if (m_btnGoBack != null)
                m_btnGoBack.onClick.RemoveAllListeners();
        }

        private void OnBtnGoBack()
        {
            Debug.Log("OnBtnGoBack  ");
            UIManager.Instance.GoBackPage();
        }

        public sealed override void Open(object args = null)
        {
            Debug.Log("Override Open  ");
            m_openArgs = args;
            m_isOpenedOnce = false;

            if(!this.gameObject.activeSelf)
                this.gameObject.SetActive(true);

            OnOpen(args);
            m_isOpenedOnce = true;
        }

        public sealed override void Close()
        {
            Debug.Log("Override Close  ");
            if(this.gameObject.activeSelf)
                this.gameObject.SetActive(false);

            OnClose();
        }
    }
}


