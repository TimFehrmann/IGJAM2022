public interface IPlaceableBehaviour
{
    void Place();
    void Despawn();
    void OnPlacementUpdate();
    bool IsPlaced();
}