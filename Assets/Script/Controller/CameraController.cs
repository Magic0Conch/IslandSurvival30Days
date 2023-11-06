using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;   //玩家
    public Camera playerCamera;        //主相机
    public Vector2 boxSize;            //视野范围
    public bool cameraMove;
    public void FollowPlayer()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.08f);//插值进行移动
        float distance = Vector3.Distance(targetPos, transform.position);
        if (distance < 0.5f)//距离小于0.5f时 停止移动
        {
            cameraMove = false;
        }
    }

    public void CheckBoundary()
    {
        float leftDistance = 0; //左右距离
        if (player.position.x < transform.position.x) //在左边
        {
            leftDistance = transform.position.x - player.position.x;
        }
        else
        {
            leftDistance = player.position.x - transform.position.x;
        }
        if (leftDistance > boxSize.x * 0.5f)//如果左右距离大于设置好的矩形宽度的一半
        {
            cameraMove = true;//相机开始移动
        }
        float uDDistance = 0;   //上下距离
        if (player.position.y < transform.position.y)
        {
            uDDistance = transform.position.y - player.position.y;
        }
        else
        {
            uDDistance = player.position.y - transform.position.y;
        }

        if (uDDistance > boxSize.y * 0.5f)
        {
            cameraMove = true;
        }
    }
    private void FixedUpdate()
    {
        CheckBoundary();
        if (cameraMove&&BasicData.Instance.cameraFollow)
        {
            FollowPlayer();
        }
    }
}
