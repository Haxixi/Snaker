using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snaker.Service.Core
{

    public static class GlobalEvent
    {

        public static ModuleEvent<bool> onLogin = new ModuleEvent<bool>();
        public static ModuleEvent<bool> onPay = new ModuleEvent<bool>();

        public static void Foo()
        {
            onLogin.Invoke(true);
        }
    }

}


