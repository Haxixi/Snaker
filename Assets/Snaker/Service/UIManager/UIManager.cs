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
                Debug.LogError("Open Fail  " + name);
            }

            return ui;
        }

        private void CloseAllLoadedPanels()
        {
            for (int i = 0; i < m_listLoadedPanel.Count; i++)
            {
                if (m_listLoadedPanel[i].IsOpen)
                {
                    m_listLoadedPanel[i].Close();
                }
            }
        }


        /// <summary>
        /// 进入主PAGE
        /// 清空PAGE堆栈
        /// </summary>
        public void EnterMainPage()
        {
            m_pageTrackStack.Clear();
            OpenPageWorker(MainScene, MainPage, null);
        }

        #region UIPage

        /// <summary>
        /// 打开PAGE
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="page"></param>
        /// <param name="args"></param>
        public void OpenPage(string scene, string page, object args = null)
        {
            Debug.Log(LOG_TAG + "  " + scene + "   " + page + "    " + args);

            if (m_currentPage != null)
            {
                m_pageTrackStack.Push(m_currentPage);
            }

            OpenPageWorker(scene, page, args);
        }


        public void OpenPage(string page, object args = null)
        {
            OpenPage(MainScene, page, args);
        }

        public void GoBackPage()
        {
            Debug.Log(LOG_TAG + "  GoBackPage");
            if (m_pageTrackStack.Count > 0)
            {
                var track = m_pageTrackStack.Pop();
                OpenPageWorker(track.scene, track.name, null);
            }
            else if (m_pageTrackStack.Count == 0)
            {
                EnterMainPage();
            }
        }


        private void OpenPageWorker(string scene, string page, object args)
        {
            Debug.Log(LOG_TAG + "  " + scene + "   " + page + "   " + args);

            string oldScene = SceneManager.GetActiveScene().name;

            m_currentPage = new UIPageTrack();
            m_currentPage.scene = scene;
            m_currentPage.name = page;

            //关闭当前PAGE打开的所有UI
            CloseAllLoadedPanels();

            if (oldScene == scene)
            {
                Open<UIPage>(page, args);
            }
            else
            {
                sceneLoaded = sceneName =>
                 {
                     if (sceneName == scene)
                     {
                         sceneLoaded = null;
                         Open<UIPage>(page, args);
                     }

                 };

                SceneManager.LoadScene(scene);
            }
        }

        #endregion

        #region UIWindow

        public UIWindow OpenWindow(string name, object args = null)
        {
            UIWindow ui = Open<UIWindow>(name, args);
            return ui;
        }

        public T OpenWindow<T>(object args = null) where T : UIWindow
        {
            T ui = Open<T>(typeof(T).Name, args);
            return ui;
        }

        #endregion

        #region UIWidget

        public UIWidget OpenWidget(string name, object args = null)
        {
            UIWidget ui = Open<UIWidget>(name, args);
            return ui;
        }

        public T OpenWidget<T>(object args = null) where T : UIWidget
        {

            T ui = Open<T>(typeof(T).Name, args);
            return ui;
        }

        #endregion
    }
}


