namespace GameItem
{
    /// <summary>
    /// Abstraction defining an interactive object
    /// </summary>
    public interface IInteractable
    {
        public bool IsActive { get; }
        
        public void ShowInteractive();
        public void HideInteractive();
    }
}