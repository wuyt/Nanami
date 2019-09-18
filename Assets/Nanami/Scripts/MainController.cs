using GoogleARCore;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Nanami
{
    /// <summary>
    /// 主要控制逻辑
    /// </summary>
    public class MainController : MonoBehaviour
    {
        /// <summary>
        /// 状态枚举
        /// </summary>
        private enum Status
        {
            /// <summary>
            /// 发现定位
            /// </summary>
            finding,
            /// <summary>
            /// 等待
            /// </summary>
            waiting,
            /// <summary>
            /// 追踪
            /// </summary>
            tracking
        }
        /// <summary>
        /// 状态
        /// </summary>
        private Status status;
        /// <summary>
        /// 发现定位界面
        /// </summary>
        private VideoPlayer findUI;
        /// <summary>
        /// 等待界面
        /// </summary>
        private GameObject waitUI;
        /// <summary>
        /// 识别图片列表
        /// </summary>
        private List<AugmentedImage> listAugmentedImage
            = new List<AugmentedImage>();
        /// <summary>
        /// 模型字典
        /// </summary>
        private Dictionary<int, Transform> dictRoom
                = new Dictionary<int, Transform>();
        /// <summary>
        /// 等待时间
        /// </summary>
        private float waitTime;
        /// <summary>
        /// 锚点
        /// </summary>
        private Anchor anchor;
        /// <summary>
        /// 当前定位对象
        /// </summary>
        private Transform currentLocation;
        /// <summary>
        /// 显示锚点情况
        /// </summary>
        private GameObject anchorShow;
        /// <summary>
        /// 导航UI
        /// </summary>
        private GameObject navUI;

        void Start()
        {
            status = Status.finding;
            findUI = GameObject.Find("/Canvas/RawImageFind").GetComponent<VideoPlayer>();
            waitUI = GameObject.Find("/Canvas/RawImageWait");
            navUI = GameObject.Find("/CanvasNavigation");
            StartFind();
        }
        void Update()
        {
            switch (status)
            {
                case Status.finding:
                    Finding();
                    break;
                case Status.waiting:
                    Waiting();
                    break;
                case Status.tracking:
                    break;
            }
            //退出应用
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        /// <summary>
        /// 开始查找定位
        /// </summary>
        private void StartFind()
        {
            findUI.gameObject.SetActive(true);
            findUI.Play();
            waitUI.SetActive(false);
            navUI.SetActive(false);
        }
        /// <summary>
        /// 查找过程
        /// </summary>
        private void Finding()
        {
            //检查是否处于追踪状态
            if (Session.Status != SessionStatus.Tracking)
            {
                return;
            }

            //更新识别图片列表
            Session.GetTrackables<AugmentedImage>(
                listAugmentedImage, TrackableQueryFilter.Updated);

            //遍历识别图片列表
            foreach (var image in listAugmentedImage)
            {
                dictRoom.TryGetValue(image.DatabaseIndex, out Transform outTransform);
                if (image.TrackingState == TrackingState.Tracking && outTransform == null)
                {
                    //图片被识别，建立锚点
                    anchor = image.CreateAnchor(image.CenterPose);
                    dictRoom.Add(image.DatabaseIndex,
                        GameObject.Find("Map/Locations/" + image.Name).transform);
                    currentLocation = GameObject.Find("Map/Locations/" + image.Name).transform;

                    //显示锚点情况
                    anchorShow = GameObject.Find("AnchorShow");
                    if (anchorShow != null)
                    {
                        anchorShow.transform.parent = anchor.transform;
                        anchorShow.transform.localPosition = Vector3.zero;
                        anchorShow.transform.localRotation = Quaternion.Euler(90f, 0, 0);
                    }

                    status = Status.waiting;
                    StartWait();
                }
                else if (image.TrackingState == TrackingState.Stopped && outTransform != null)
                {
                    dictRoom.Remove(image.DatabaseIndex);
                }
            }
        }
        /// <summary>
        /// 开始等待
        /// </summary>
        private void StartWait()
        {
            waitTime = Time.time;
            findUI.Stop();
            findUI.gameObject.SetActive(false);
            waitUI.SetActive(true);
        }
        /// <summary>
        /// 等待过程
        /// </summary>
        private void Waiting()
        {
            //延时2秒
            if (Time.time - waitTime < 2f)
            {
                return;
            }
            //检查是否处于追踪状态
            if (Session.Status == SessionStatus.Tracking)
            {
                //进入追踪状态
                SetMapInfo();

                status = Status.tracking;
                StartTrack();
            }
            else
            {
                dictRoom.Clear();
                status = Status.finding;
                StartFind();
            }
        }
        /// <summary>
        /// 设置地图信息
        /// </summary>
        private void SetMapInfo()
        {
            //修改导航空间位置
            Transform map = GameObject.Find("Map").transform;
            if (currentLocation != null && map != null)
            {
                map.position = anchor.transform.position;
                map.eulerAngles = anchor.transform.eulerAngles;
                map.Rotate(90f, 0, 0, Space.Self);
                map.position = map.position - currentLocation.localPosition;
                anchorShow.transform.parent = null;
            }
        }
        /// <summary>
        /// 开始追踪
        /// </summary>
        private void StartTrack()
        {
            waitUI.SetActive(false);
            navUI.SetActive(true);
        }
    }
}

