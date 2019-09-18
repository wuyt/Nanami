using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nanami
{
    /// <summary>
    /// 调试模式开关
    /// </summary>
    public class DebugSwitch : Editor
    {
        /// <summary>
        /// 打开调试模式
        /// </summary>
        [MenuItem("Nanami/Enable Debug")]
        static void EnableDebug()
        {
            Debug.Log("Enable");
            SetDebug(true);

        }
        /// <summary>
        /// 关闭调试模式
        /// </summary>
        [MenuItem("Nanami/Disable Debug")]
        static void DisableDebug()
        {
            Debug.Log("Disable");
            SetDebug(false);

        }
        /// <summary>
        /// 设置调试对象
        /// </summary>
        /// <param name="status">状态：true（调式模式）；false（运行模式）</param>
        static void SetDebug(bool status)
        {
            //遍历根节点游戏对象
            var gos = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var go in gos)
            {
                switch (go.name)
                {
                    case "PointCloud":
                        //如果是显示点云的游戏对象
                        go.SetActive(status);
                        break;
                    case "AnchorShow":
                        //如果是锚点显示游戏对象
                        for (int i = 0; i < go.transform.childCount; i++)
                        {
                            go.transform.GetChild(i).gameObject.SetActive(status);
                        }
                        break;
                    case "Map":
                        SetMap(go.transform, status);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 设置和环境对应的游戏对象
        /// </summary>
        /// <param name="tf">和环境对应的游戏对象</param>
        /// <param name="status">状态：true（调式模式）；false（运行模式）</param>
        static void SetMap(Transform tf, bool status)
        {
            for (int i = 0; i < tf.childCount; i++)
            {
                switch (tf.GetChild(i).name)
                {
                    case "Center":
                        tf.GetChild(i).gameObject.SetActive(status);
                        break;
                    case "Locations":
                        SetChildMesh(tf.GetChild(i), status);
                        break;
                    case "WallPoints":
                        tf.GetChild(i).gameObject.SetActive(status);
                        break;
                    case "Ground":
                        SetChildMesh(tf.GetChild(i), status);
                        break;
                    case "Roads":
                        SetChildMesh(tf.GetChild(i), status);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 设置子对象的渲染器
        /// </summary>
        /// <param name="tf">游戏对象</param>
        /// <param name="status">状态</param>
        static void SetChildMesh(Transform tf, bool status)
        {
            for (int i = 0; i < tf.childCount; i++)
            {
                tf.GetChild(i).GetComponent<MeshRenderer>().enabled = status;
            }
        }
    }
}