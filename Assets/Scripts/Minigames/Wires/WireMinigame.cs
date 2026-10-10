using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WireMiniGame : MonoBehaviour
{
    public CubeFace cubeFace;
    private GameController gameController;
    private WireDrag CurrentWire;

    private WireDragSpot[] WireSpots;
    private void Awake()
    {
        WireSpots = this.gameObject.GetComponentsInChildren<WireDragSpot>();
    }

    private void Start()
    {
        gameController = GameObject.Find("Main Camera").GetComponent<GameController>();
    }

    public void SetCurrentWire(WireDrag wire)
    {
        CurrentWire = wire;
    }

    public WireDrag GetCurrentWire()
    {
        return CurrentWire;
    }

    public void CheckComplete()
    {
        foreach (WireDragSpot wire in WireSpots) {
            if (wire.canDraw) return;
        }
        gameController.ZoomOutFromCube();
        cubeFace.RepairComplete();
    }
}