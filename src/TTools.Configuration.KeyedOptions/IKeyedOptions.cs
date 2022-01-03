namespace TTools.Configuration.KeyedOptions
{
    /// <summary>
    /// An interface allowing options to provide their own section key
    /// </summary>
    public interface IKeyedOptions
    {
        /// <summary>
        /// The section key in the appsettings file
        /// (e.g. {"MySection":{"A": "B"}} would be "MySection")
        /// </summary>
        public string SectionKey { get; }
    }
}
