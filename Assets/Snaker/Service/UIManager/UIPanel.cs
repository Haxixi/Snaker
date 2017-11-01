using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snaker.Service.UIManager
{
    public abstract class UIPanel : MonoBehaviour
    {
        /// <summary>
        /// UI框架逻辑，区分UI业务逻辑
        /// </summary>
        /// <param name="args"></param>
        public virtual void Open(object args = null)
        {
            Debug.Log(this.GetType().Name + "  Open  " + args);
        }

        public virtual void Close()
        {
            Debug.Log(this.GetType().Name + "  Close  ");
        }

        public bool IsOpen
        {
            get { return this.gameObject.activeSelf; }
        }

        protected virtual void OnClose()
        {
            Debug.Log(this.GetType().Name + "  OnClose  ");
        }

        /// <summary>
        /// UI业务逻辑，派生类做一些数据处理
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnOpen(object args = null)
        {
            Debug.Log(this.GetType().Name + "  OnOpen  " + args);
        }
    }
}


