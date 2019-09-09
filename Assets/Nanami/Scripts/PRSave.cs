

using UnityEngine;

namespace Nanami
{
    /// <summary>
    /// 坐标角度保存
    /// </summary>
    public class PRSave : MonoBehaviour
    {
        void Start()
        {
            //读取位置信息
            transform.localPosition = new Vector3(
                PlayerPrefs.GetFloat(name + "px", transform.localPosition.x),
                PlayerPrefs.GetFloat(name + "py", transform.localPosition.y),
                PlayerPrefs.GetFloat(name + "pz", transform.localPosition.z));
            transform.localEulerAngles = new Vector3(
                PlayerPrefs.GetFloat(name + "rx", transform.localEulerAngles.x),
                PlayerPrefs.GetFloat(name + "ry", transform.localEulerAngles.y),
                PlayerPrefs.GetFloat(name + "rz", transform.localEulerAngles.z));
        }
        /// <summary>
        /// 保存位置信息
        /// </summary>
        public void Save()
        {
            PlayerPrefs.SetFloat(name + "px", transform.localPosition.x);
            PlayerPrefs.SetFloat(name + "py", transform.localPosition.y);
            PlayerPrefs.SetFloat(name + "pz", transform.localPosition.z);
            PlayerPrefs.SetFloat(name + "rx", transform.localEulerAngles.x);
            PlayerPrefs.SetFloat(name + "ry", transform.localEulerAngles.y);
            PlayerPrefs.SetFloat(name + "rz", transform.localEulerAngles.z);
        }
        /// <summary>
        /// 清除位置信息
        /// </summary>
        public void Clear()
        {
            PlayerPrefs.DeleteKey(name + "px");
            PlayerPrefs.DeleteKey(name + "py");
            PlayerPrefs.DeleteKey(name + "pz");
            PlayerPrefs.DeleteKey(name + "rx");
            PlayerPrefs.DeleteKey(name + "ry");
            PlayerPrefs.DeleteKey(name + "rz");
        }
    }
}

