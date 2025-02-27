using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Network : MonoBehaviourPunCallbacks
{

    public Camera_Movement camera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Connecting");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 5000);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinOrCreateRoom("Room" + Random.Range(0, 5000),
            new RoomOptions() { MaxPlayers = 8 }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected");
        camera.player = PhotonNetwork.Instantiate("player",
            new Vector3(
                Random.Range(-10, 10),
                Random.Range(-10, 10),
                0), Quaternion.identity).transform;
    }
}
