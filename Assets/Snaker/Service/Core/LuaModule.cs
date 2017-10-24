using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snaker.Service.Core
{

    public class LuaModule : BusinessModule
    {
        private object m_args = null;

        internal LuaModule(string name) : base(name)
        {
            
        }

        public override void Create(object args = null)
        {
            base.Create(args);
            m_args = args;
            //加载Name对应的Lua脚本
        }

        public override void Release()
        {
            base.Release();
            //释放Name对应的Lua脚本
        }
    }
}

