using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace Snaker.Service.Core
{
    public abstract class BusinessModule : Module
    {
        private string m_name = "";

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(m_name))
                    m_name = this.GetType().Name;//通过反射获取类名

                return m_name;
            }
        }

        public string Title;


        public BusinessModule()
        {

        }

        internal BusinessModule(string name)
        {
            m_name = name;
        }

        private EventTable m_tblEvent;

        internal void SetEventTable(EventTable tblEvent)
        {
            m_tblEvent = tblEvent;
        }

        public ModuleEvent Event(string type)
        {
            return GetEventTable().GetEvent(type);
        }

        protected EventTable GetEventTable()
        {
            if (m_tblEvent == null)
                m_tblEvent = new EventTable();

            return m_tblEvent;
        }


        //private void QTest()
        //{
        //    BusinessModule mdlLogin = null;

        //    mdlLogin.Event("Login").AddListener(OnLogin);
        //}

        //private void OnLogin(object target)
        //{
        //    Debug.Log("QTest");
        //}

        internal void HandleMessage(string msg, object[] args)
        {
            Debug.Log("HandleMessage   " + msg + "  " + args);

            MethodInfo mi = this.GetType().GetMethod(msg, BindingFlags.NonPublic | BindingFlags.Instance);
            if (mi != null)
            {
                mi.Invoke(this, BindingFlags.NonPublic, null, args, null);//用反射通过msg的消息名字获取函数并调用
            }
            else
            {
                OnModuleMessage(msg, args);//如果找不到msg消息名字函数就调用通用的函数
            }
        }

        protected virtual void OnModuleMessage(string msg, object[] args)
        {
            Debug.Log("OnModuleMessage   " + msg + "  " + args);
        }

        public virtual void Create(object args = null)
        {
            Debug.Log(args);
        }

        public override void Release()
        {
            if (m_tblEvent != null)
            {
                m_tblEvent.Clear();
                m_tblEvent = null;
            }

            base.Release();
        }


        public virtual void Show()
        {
            Debug.Log("Show");
        }
    }
}


