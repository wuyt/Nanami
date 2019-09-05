using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;

namespace Nanami
{
    /// <summary>
    /// ARCore错误提示
    /// </summary>
    public class ARCoreErrorController : MonoBehaviour
    {
        /// <summary>
        /// 异常显示文本
        /// </summary>
        private Text text;

        void Start()
        {
            text = GameObject.Find("/Canvas/TextError").GetComponent<Text>();
            text.gameObject.SetActive(false);
        }

        void Update()
        {
            //处于追踪状态不会进入睡眠模式
            if (Session.Status != SessionStatus.Tracking)
            {
                Screen.sleepTimeout = 15;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            //显示异常后不继续操作
            if (text.gameObject.activeSelf)
            {
                return;
            }
            //如果有异常则显示
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                text.gameObject.SetActive(true);
                text.text = "Camera permission is needed.";
            }
            else if (Session.Status.IsError())
            {
                text.gameObject.SetActive(true);
                text.text = "ARCore encountered a problem connecting.";
            }
        }
    }
}