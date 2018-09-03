namespace Dxflib.IO.Header
{
    /// <inheritdoc />
    /// <summary>
    ///     The String Variable Class for AutoCAD File Variables.
    ///     This class is a specialization of the <see cref="T:Dxflib.IO.Header.FileVariable`1" />
    ///     class where T is a <see cref="T:System.String" />.
    /// </summary>
    public sealed class StringVar : FileVariable<string>
    {
        /// <inheritdoc />
        /// <summary>
        ///     The main constructor for a string variable
        /// </summary>
        /// <param name="variableName">
        ///     The file variable name eg. "$LASTSAVEDBY"
        ///     The last saved by file variable
        /// </param>
        /// <param name="value">The value of the variable</param>
        public StringVar(string variableName, string value) : base(variableName)
        {
            // Set the value
            Value = value;
        }
    }
}