using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snaker.Service.UIManager
{
    public abstract class UIPanel : MonoBehaviour
    {

        public virtual void Open(object args = null)
        {
            Debug.Log("Open  " + args);
        }

        public virtual void Close()
        {
            Debug.Log("Close  ");
        }

        public bool IsOpen
        {
            get { return this.gameObject.activeSelf; }
        }

        protected virtual void OnClose()
        {
            Debug.Log("OnClose  ");
        }

        protected virtual void OnOpen(object args = null)
        {
            Debug.Log("OnOpen  " + args);
        }
    }
}


