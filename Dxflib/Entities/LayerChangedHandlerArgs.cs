namespace Dxflib.Entities
{
    /// <summary>
    ///     Handler Arguments for the LayerChanged Event
    /// </summary>
    public class LayerChangedHandlerArgs
    {
        /// <summary>
        ///     The Main Constructor for the layer changed event
        /// </summary>
        /// <param name="oldName">The Old Layer Name</param>
        /// <param name="newName">The New Layer Name</param>
        public LayerChangedHandlerArgs(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }

        /// <summary>
        ///     The Old Layer Name
        /// </summary>
        public string OldName { get; }

        /// <summary>
        ///     The New Layer Name
        /// </summary>
        public string NewName { get; }
    }
}