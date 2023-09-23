namespace LBD.Enums
{
    public enum SingleParameterEvents
    {
        /// <summary>
        /// When a action is prepared but not yet confirmed, uses a ActionSpeedEnum parameter
        /// </summary>
        OnActionPrepared,
        /// <summary>
        /// When prepared action is performed, uses a ActionSpeedEnum parameter
        /// </summary>
        OnActionPerformed
    }
}