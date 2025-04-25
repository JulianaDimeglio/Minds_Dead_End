using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] string roomName;

    //make getters
    public string RoomName => roomName;
}
