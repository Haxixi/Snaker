using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snaker.Service.Core
{

    public abstract class Module
    {

        public virtual void Release()
        {
            Debug.Log("Release");
        }
    }
}


