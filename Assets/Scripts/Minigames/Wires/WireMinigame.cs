using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WireMiniGame : MonoBehaviour
{

    private WireDrag CurrentWire;

    private WireDragSpot[] WireSpots;
    private void Awake()
    {
        WireSpots = this.gameObject.GetComponentsInChildren<WireDragSpot>();
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
        Debug.Log("Minigame done");
    }
}