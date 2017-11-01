using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snaker.Service.UIManager
{

    public class UIRoot : MonoBehaviour
    {

        public const string LOG_TAG = "UIRoot";

        /// <summary>
        /// UIRoot下通过类型寻找一个组件对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Find<T>() where T : MonoBehaviour
        {
            string name = typeof(T).Name;

            GameObject obj = Find(name);

            if (obj != null)
                return obj.GetComponent<T>();

            return null;
        }

        /// <summary>
        /// UIRoot下通过类型和名字寻找一个组件对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Find<T>(string name) where T : MonoBehaviour
        {
            GameObject obj = Find(name);

            if (obj != null)
                return obj.GetComponent<T>();

            return null;
        }

        /// <summary>
        /// UIRoot下通过名字寻找一个组件对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject Find(string name)
        {
            Transform obj = null;
            GameObject root = FindUIRoot();
            if (root != null)
                obj = root.transform.Find(name);
            if (obj != null)
                return obj.gameObject;

            return null;
        }

        /// <summary>
        /// 寻找UIRoot对象
        /// </summary>
        /// <returns></returns>
        public static GameObject FindUIRoot()
        {
            GameObject root = GameObject.Find("UIRoot");
            if (root != null && root.GetComponent<UIRoot>() != null)
            {
                return root;
            }
            return null;
        }

        /// <summary>
        /// UIRoot添加子对象
        /// </summary>
        /// <param name="child"></param>
        public static void AddChild(UIPanel child)
        {
            GameObject root = FindUIRoot();
            if (root == null || child == null)
                return;

            child.transform.SetParent(root.transform, false);
        }
    }
}


