public interface IInventoryReceiver
{
    bool CanReceive { get; }
    bool TryReceiveItem(ItemData item);
    
}
