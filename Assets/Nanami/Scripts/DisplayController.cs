using UnityEngine;

namespace Nanami
{
    /// <summary>
    /// 显示子内容
    /// </summary>
    public class DisplayController : MonoBehaviour
    {
        /// <summary>
        /// 子游戏对象
        /// </summary>
        private GameObject child;
        private void Start()
        {
            if (transform.childCount == 1)
            {
                child = transform.GetChild(0).gameObject;
                child.SetActive(false);
            }
        }

        /// <summary>
        /// 进入事件
        /// </summary>
        /// <param name="other">进入的游戏对象</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "First Person Camera")
            {
                child.SetActive(true);
            }
        }
        /// <summary>
        /// 离开事件
        /// </summary>
        /// <param name="other">进入的游戏对象</param>
        private void OnTriggerExit(Collider other)
        {
            if (other.name == "First Person Camera")
            {
                child.SetActive(false);
            }
        }
    }
}

