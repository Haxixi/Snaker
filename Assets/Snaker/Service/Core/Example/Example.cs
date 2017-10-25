using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snaker.Service.Core
{
    public class ModuleA : BusinessModule
    {
        public override void Create(object args = null)
        {
            base.Create(args);

            //业务模块之间通过消息进行通讯
            ModuleManager.Instance.SendMessage("ModuleB", "Message1", "NTG", "MSS", 123);
            ModuleManager.Instance.SendMessage("ModuleB", "Message2", 5233);

            //业务模块之间通过事件进行通讯
            ModuleManager.Instance.Event("ModuleB", "onModuleEventB").AddListener(OnModuleEventB);

            //业务层调用服务层,通过事件监听回掉
            ModuleC.Instance.onEvent.AddListener(OnModuleEventC);
            ModuleC.Instance.DoSomething();

            //全局事件
            GlobalEvent.onLogin.AddListener(OnLogin);
            GlobalEvent.Foo();
        }

        private void OnModuleEventC(object args)
        {
            Debug.Log("OnModuleEventC   " + args);
        }

        private void OnModuleEventB(object args)
        {
            Debug.Log("OnModuleEventB   " + args);
        }

        private void OnLogin(bool args)
        {
            Debug.Log("OnLogin   " + args);
        }
    }

    public class ModuleB : BusinessModule
    {
        public ModuleEvent onModuleEventB
        {
            get { return Event("onModuleEventB"); }
        }

        public override void Create(object args = null)
        {
            base.Create(args);
            onModuleEventB.Invoke("I Love MSS");
        }

        protected void Message1(string args1, string args2, int args3)
        {
            Debug.Log("Message1   " + args1 + " " + args2 + " " + args3);
        }

        protected void Message2(int args1)
        {
            Debug.Log("Message2   " + args1);
        }

        protected override void OnModuleMessage(string msg, object[] args)
        {
            base.OnModuleMessage(msg, args);
            Debug.Log("OnModuleMessage   " + msg + "  " + args.Length);
        }
    }

    public class ModuleC : ServiceModule<ModuleC>
    {
        public ModuleEvent onEvent = new ModuleEvent();

        public void Init()
        {

        }

        public void DoSomething()
        {
            onEvent.Invoke(null);
        }
    }

    public class Example
    {

        // Use this for initialization
        public void Init()
        {
            ModuleC.Instance.Init();
            ModuleManager.Instance.Init("Snaker.Service.Core");

            ModuleManager.Instance.CreateModule("ModuleA");
            ModuleManager.Instance.CreateModule("ModuleB");
        }
    }
}


