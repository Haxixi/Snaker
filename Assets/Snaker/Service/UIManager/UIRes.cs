using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snaker.Service.UIManager
{

    public static class UIRes
    {

        public static string UIResRoot = "UI/";

        public static GameObject LoadPrefab(string name)
        {
            GameObject asset = (GameObject) Resources.Load(UIResRoot + name);
            return asset;
        }
    }
}


