namespace Edgar.Unity
{
    /// <summary>
    /// Transition mode of Fog of War.
    /// </summary>
    public enum FogOfWarTransitionMode
    {
        /// <summary>
        /// Fog values of neighboring tiles are interpolated, resulting in smooth transitions.
        /// </summary>
        Smooth = 0,

        /// <summary>
        /// Fog values of neighboring tiles are NOT interpolated, resulting in tile-based transitions.
        /// </summary>
        TileBased = 1,
    }
}