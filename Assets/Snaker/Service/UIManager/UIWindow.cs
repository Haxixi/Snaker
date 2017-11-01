using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Snaker.Service.UIManager
{
    public class UIWindow : UIPanel
    {
        public delegate void CloseEvent();

        [SerializeField] private Button m_btnClose;

        public event CloseEvent onClose;

        protected object m_openArg;

        private bool m_isOpenedOnce;

        protected void OnEnable()
        {
            Debug.Log(this.GetType().Name + "  OnEnable  ");

            if (m_btnClose != null)
            {
                m_btnClose.onClick.AddListener(OnBtnClose);
            }

#if UNITY_EDITOR
            if (m_isOpenedOnce)
                OnOpen(m_openArg);
#endif
        }

        protected void OnDisable()
        {
            Debug.Log(this.GetType().Name + "  OnDisable");

#if UNITY_EDITOR
            if (m_isOpenedOnce)
            {
                OnClose();
                if (onClose != null)
                {
                    onClose();
                    onClose = null;
                }
            }
#endif
            if (m_btnClose != null)
            {
                m_btnClose.onClick.RemoveAllListeners();
            }
        }

        private void OnBtnClose()
        {
            Debug.Log(this.GetType().Name + "  OnBtnClose");
            Close();
        }

        public sealed override void Open(object args = null)
        {
            Debug.Log(this.GetType().Name + "  Open" + args);
            m_openArg = args;
            m_isOpenedOnce = false;
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(args);
            m_isOpenedOnce = true;
        }

        public sealed override void Close()
        {
            Debug.Log(this.GetType().Name+"  Close");
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose();
            if (onClose != null)
            {
                onClose();
                onClose = null;
            }
        }
    }
}


