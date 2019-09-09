using UnityEngine;
using UnityEngine.UI;

namespace Nanami
{
    /// <summary>
    /// 矫正相关控制
    /// </summary>
    public class ReviseController : MonoBehaviour
    {
        /// <summary>
        /// 画布
        /// </summary>
        public GameObject canvas;
        /// <summary>
        /// 当前对象
        /// </summary>
        private Transform currentTransform;
        /// <summary>
        /// 信息文本
        /// </summary>
        public Text textInfo;
        /// <summary>
        /// 设置文本
        /// </summary>
        public Text textSet;
        /// <summary>
        /// 输入值
        /// </summary>
        public InputField inputNumber;
        /// <summary>
        /// 设置类型：true（position），false（rotation）
        /// </summary>
        private bool modifyType;
        /// <summary>
        /// 轴：-1（X），0（Y），1（Z）
        /// </summary>
        private int axis;
        /// <summary>
        /// 运算符：true（+），false（-）
        /// </summary>
        private bool operation;

        void Start()
        {
            canvas.SetActive(false);
            modifyType = true;
            axis = -1;
            operation = true;
        }

        void Update()
        {
            //只有一个触摸点才继续
            if (Input.touchCount != 1)
            {
                return;
            }

            //触碰发生时才继续
            if (Input.GetTouch(0).phase != TouchPhase.Began)
            {
                return;
            }

            //射线检测是否点击了物体
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            int mask = 1 << 9;
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, mask))
            {
                currentTransform = hit.transform;
                UpdateUI();
            }
        }
        /// <summary>
        /// 更新显示
        /// </summary>
        private void UpdateUI()
        {
            //当前对象不能为空
            if (currentTransform == null)
            {
                return;
            }
            //显示UI
            canvas.SetActive(true);
            //显示对象名称和位置信息
            textInfo.text = currentTransform.name;
            textInfo.text = currentTransform.name + "\r\n"
                + currentTransform.localPosition.ToString() + "\r\n"
                + currentTransform.localEulerAngles.ToString();
            //显示设置
            //类型
            if (modifyType)
            {
                textSet.text = "position->";
            }
            else
            {
                textSet.text = "rotation->";
            }
            //轴
            switch (axis)
            {
                case -1:
                    textSet.text += "X";
                    break;
                case 0:
                    textSet.text += "Y";
                    break;
                case 1:
                    textSet.text += "Z";
                    break;
            }
            //运算符
            if (operation)
            {
                textSet.text += "+";
            }
            else
            {
                textSet.text += "-";
            }
        }
        /// <summary>
        /// 清除所有设置
        /// </summary>
        public void ClearAll()
        {
            PlayerPrefs.DeleteAll();
        }
        /// <summary>
        /// 清除当前对象的设置
        /// </summary>
        public void Clear()
        {
            currentTransform.SendMessage("Clear");
        }
        /// <summary>
        /// 保存当前对象设置
        /// </summary>
        public void Save()
        {
            currentTransform.SendMessage("Save");
        }
        /// <summary>
        /// 关闭UI
        /// </summary>
        public void Close()
        {
            canvas.SetActive(false);
        }
        /// <summary>
        /// 设置类型
        /// </summary>
        /// <param name="inputType">输入类型</param>
        public void SetType(bool inputType)
        {
            modifyType = inputType;
            UpdateUI();
        }
        /// <summary>
        /// 设置轴
        /// </summary>
        /// <param name="inputAxis">输入轴</param>
        public void SetAxis(int inputAxis)
        {
            axis = inputAxis;
            UpdateUI();
        }
        /// <summary>
        /// 设置运算符
        /// </summary>
        /// <param name="inputOperation">输入运算符</param>
        public void SetOperation(bool inputOperation)
        {
            operation = inputOperation;
            UpdateUI();
        }
        /// <summary>
        /// 设置当前对象位置信息
        /// </summary>
        public void SetTransform()
        {
            float num = float.Parse(inputNumber.text);
            if (modifyType)
            {
                if (operation)
                {
                    ModifyPosition(num);
                }
                else
                {
                    ModifyPosition(-num);
                }
            }
            else
            {
                if (operation)
                {
                    ModifyRotation(num);
                }
                else
                {
                    ModifyRotation(-num);
                }
            }
            UpdateUI();
        }
        /// <summary>
        /// 修改坐标
        /// </summary>
        /// <param name="num">修改值</param>
        private void ModifyPosition(float num)
        {
            Vector3 oldPosition = currentTransform.localPosition;
            switch (axis)
            {
                case -1:
                    currentTransform.localPosition = new Vector3(
                        oldPosition.x + num,
                        oldPosition.y,
                        oldPosition.z);
                    break;
                case 0:
                    currentTransform.localPosition = new Vector3(
                        oldPosition.x,
                        oldPosition.y + num,
                        oldPosition.z);
                    break;
                case 1:
                    currentTransform.localPosition = new Vector3(
                        oldPosition.x,
                        oldPosition.y,
                        oldPosition.z + num);
                    break;
            }
        }
        /// <summary>
        /// 修改角度
        /// </summary>
        /// <param name="num">修改值</param>
        private void ModifyRotation(float num)
        {
            Vector3 oldRotation = currentTransform.localEulerAngles;
            switch (axis)
            {
                case -1:
                    currentTransform.localEulerAngles = new Vector3(
                        oldRotation.x + num,
                        oldRotation.y,
                        oldRotation.z);
                    break;
                case 0:
                    currentTransform.localEulerAngles = new Vector3(
                        oldRotation.x,
                        oldRotation.y + num,
                        oldRotation.z);
                    break;
                case 1:
                    currentTransform.localEulerAngles = new Vector3(
                        oldRotation.x,
                        oldRotation.y,
                        oldRotation.z + num);
                    break;
            }
        }
    }
}