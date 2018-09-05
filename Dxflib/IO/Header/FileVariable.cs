namespace Dxflib.IO.Header
{
    /// <summary>
    ///     A generic class that is used to act as a base for
    ///     other file variable classes to specialize and inherit from
    /// </summary>
    /// <typeparam name="T">The Typename of the file variable</typeparam>
    public class FileVariable<T>
    {
        /// <summary>
        ///     The main constructor for the FileVariable Class.
        ///     This Constructor will set the variable name.
        /// </summary>
        /// <param name="variableName"></param>
        protected FileVariable(string variableName) { VariableName = variableName; }

        /// <summary>
        ///     The Variable Name
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string VariableName { get; }

        /// <summary>
        ///     The Variables Value
        /// </summary>
        public virtual T Value { get; set; }
    }
}