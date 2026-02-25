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

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTarget();
            timer = 0f;
        }
    }

    void SpawnTarget()
    {
        // 在单位球面上取随机方向
        Vector3 dir = Random.onUnitSphere;

        // 可选：限制生成高度范围，避免出现在脚下
        dir.y = Mathf.Clamp(dir.y, -0.2f, 0.8f);

        Vector3 spawnPos = xrOrigin.position + dir.normalized * radius;

        GameObject target = Instantiate(targetPrefab, spawnPos, Quaternion.identity);

        // 面朝玩家（通常用头显摄像机）
        Transform cam = Camera.main.transform;
        target.transform.LookAt(cam.position);

        // 如果模型朝向反了，改用下面这行
        // target.transform.forward = (cam.position - spawnPos).normalized;
    }
}