using System;
using System.Collections;
using System.Collections.Generic;
using Snaker.Service.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snaker.Service.UIManager
{

    public class UIManager : ServiceModule<UIManager>
    {

        public const string LOG_TAG = "UIManager";

        public static string MainScene = "Main";
        public static string MainPage = "UIMainPage";


        class UIPageTrack
        {
            public string name;
            public string scene;
        }

        private Stack<UIPageTrack> m_pageTrackStack;
        private UIPageTrack m_currentPage;
        private Action<string> sceneLoaded;
        private List<UIPanel> m_listLoadedPanel;

        public UIManager()
        {
            m_pageTrackStack = new Stack<UIPageTrack>();
            m_listLoadedPanel = new List<UIPanel>();
        }

        public void Init(string uiResRoot)
        {
            CheckSingleton();

            UIRes.UIResRoot = uiResRoot;


            //监听UnityScene加载事件(Unity自带的加载监听无法置空，所以需要自己覆写事件)
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (sceneLoaded != null)
                {
                    sceneLoaded(scene.name);
                }
            };
        }

        /// <summary>
        /// 加载UI，如果UIRoot下已经存在，直接获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private T Load<T>(string name) where T : UIPanel
        {
            T ui = UIRoot.Find<T>(name);

            if (ui == null)
            {
                GameObject original = UIRes.LoadPrefab(name);
                if (original != null)
                {
                    GameObject go = GameObject.Instantiate(original);
                    ui = go.GetComponent<T>();
                    if (ui != null)
                    {
                        go.name = name;
                        UIRoot.AddChild(ui);
                    }
                    else
                    {
                        Debug.LogError("Prefab没有对应组件" + name);
                    }
                }

                else
                {
                    Debug.LogError("Res Not Found  " + name);
                }
            }

            if (ui != null)
            {
                if (m_listLoadedPanel.IndexOf(ui) < 0)
                {
                    m_listLoadedPanel.Add(ui);
                }
            }
            
            return ui;
        }

        private T Open<T>(string name, object args = null) where T : UIPanel
        {
            T ui = Load<T>(name);
            if (ui != null)
            {
                ui.Open(args);
            }
            else
            {
                Debug.LogError();
            }
        }
    }
}


