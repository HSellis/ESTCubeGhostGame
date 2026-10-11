using UnityEngine;

public class WireMiniGame : MiniGame
{
    
    private WireDrag CurrentWire;

    private WireDragSpot[] WireSpots;
    private void Awake()
    {
        WireSpots = this.gameObject.GetComponentsInChildren<WireDragSpot>();
    }

    private void Start()
    {
        
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
        RepairComplete();
    }

    protected override void ResetMinigame()
    {
        foreach (WireDragSpot wire in WireSpots)
        {
            wire.canDraw = true;
        }
        foreach (WireDrag wireDrag in GetComponentsInChildren<WireDrag>())
        {
            Destroy(wireDrag.gameObject);
        }
    }
}