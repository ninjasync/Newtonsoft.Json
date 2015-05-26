#if !DOT42
using System;

namespace Newtonsoft.Json
{
    /// <summary>
    /// This is a Dot42 specific Attribute
    /// <para/>
    /// Specifies that a parameter is used in serialization. Types and objects passed as this 
    /// parameter will have all their public fields and public and private properties preserved
    /// and not pruned.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter|AttributeTargets.GenericParameter, Inherited = true, AllowMultiple = false)]
    internal class SerializedParameter : Attribute
    {
    }
}
#endif