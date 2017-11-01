using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snaker.Service.UIManager
{

    public class UIWidget : UIPanel
    {

        protected object m_openArgs;

        public sealed override void Open(object args = null)
        {
            Debug.Log(this.GetType().Name + "  Open  " + args);
            m_openArgs = args;
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(args);
        }

        public sealed override void Close()
        {
            Debug.Log(this.GetType().Name+"  Close  ");
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose();
        }
    }
}


