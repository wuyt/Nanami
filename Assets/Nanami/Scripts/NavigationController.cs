using UnityEngine;
using UnityEngine.AI;

namespace Nanami
{
    /// <summary>
    /// 导航控制
    /// </summary>
    public class NavigationController : MonoBehaviour
    {
        /// <summary>
        /// 菜单按钮
        /// </summary>
        public GameObject menuButton;
        /// <summary>
        /// 面板
        /// </summary>
        public GameObject panel;
        /// <summary>
        /// 导航代理
        /// </summary>
        private NavMeshAgent agent;
        /// <summary>
        /// 导航路径
        /// </summary>
        private NavMeshPath path;
        /// <summary>
        /// 导航（动态更新用）
        /// </summary>
        private NavMeshSurface surface;
        /// <summary>
        /// 导航线
        /// </summary>
        private LineRenderer line;
        /// <summary>
        /// 目标点坐标
        /// </summary>
        private Vector3 target;
        /// <summary>
        /// 玩家
        /// </summary>
        public Transform player;

        void Start()
        {
            agent = FindObjectOfType<NavMeshAgent>();
            agent.enabled = false;
            surface = FindObjectOfType<NavMeshSurface>();
            path = new NavMeshPath();
            //设置导航线的颜色宽度
            line = FindObjectOfType<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.positionCount = 0;
            line.widthMultiplier = 0.3f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] {
                new GradientColorKey(Color.blue, 0.0f),
                new GradientColorKey(Color.blue, 1.0f) },
                new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0.0f),
                new GradientAlphaKey(1f, 1.0f) });
            line.colorGradient = gradient;
        }

        /// <summary>
        /// 面板显示设置
        /// </summary>
        /// <param name="show">显示参数：true（显示）；false（隐藏）</param>
        public void SetPanelDisplay(bool display)
        {
            panel.SetActive(display);
            menuButton.SetActive(!display);
        }
        /// <summary>
        /// 构建导航网格
        /// </summary>
        public void BuildNavMesh()
        {
            surface.BuildNavMesh();
        }
        /// <summary>
        /// 导航到目标
        /// </summary>
        /// <param name="targetTF">目标对象</param>
        public void NavigationTarget(Transform targetTF)
        {
            //停止重复
            CancelInvoke("DisplayPath");

            target = targetTF.position;

            //重复开始
            InvokeRepeating("DisplayPath", 0, 0.5f);

            SetPanelDisplay(false);
        }
        /// <summary>
        /// 显示路径
        /// </summary>
        public void DisplayPath()
        {
            //将代理移动到当前位置
            agent.transform.position = player.position;
            agent.enabled = true;
            //计算路径
            agent.CalculatePath(target, path);
            //显示路径
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            //停止代理
            agent.enabled = false;
        }
    }
}