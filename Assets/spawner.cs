using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;     // 你的 Cube Target 预制体
    public Transform xrOrigin;          // XR Origin (XR Rig)
    public float radius = 15f;           // 生成半径
    public float spawnInterval = 1.5f;  // 生成间隔

    private float timer;

    void Update()
    {
        if (targetPrefab == null || xrOrigin == null) return;

        timer += Time.deltaTime; // 累积时间

        if (timer >= spawnInterval) // 达到生成间隔，生成一个目标
        {
            SpawnTarget();
            timer = 0f;
        }
    }

    void SpawnTarget()
    {
        // 在单位球面上取随机方向
        Vector3 dir = Random.onUnitSphere;

        //限制生成高度范围，避免出现在脚下.Random.onUnitSphere 生成的是完整球面方向，y 分量范围是 -1 到 1：
        dir.y = Mathf.Clamp(dir.y, 0.0f, 0.5f);

        //中心点 + 方向 × 距离
        Vector3 spawnPos = xrOrigin.position + dir.normalized * radius;

        // 生成目标，Quaternion.identity 表示不旋转
        // 在 Unity 中旋转不能用 Vector3，而必须用 Quaternion
        GameObject target = Instantiate(targetPrefab, spawnPos, Quaternion.identity);

        // 面朝玩家
        Transform cam = Camera.main.transform;
        target.transform.LookAt(cam.position);

        // 如果模型朝向反了，改用下面这行
        // target.transform.forward = (cam.position - spawnPos).normalized;
    }
}