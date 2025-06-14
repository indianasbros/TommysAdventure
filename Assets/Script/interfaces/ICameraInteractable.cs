public interface ICameraInteractable
{
    void ChangeToCamera();
    void ChangeToMainCamera();
    bool CanInteract { get; }
}
