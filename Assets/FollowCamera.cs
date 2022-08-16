using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
  [SerializeField] GameObject targetToFollow;
    void Update()
    {
        transform.position = targetToFollow.transform.position + new Vector3(0, 0, -10);
    }
}
