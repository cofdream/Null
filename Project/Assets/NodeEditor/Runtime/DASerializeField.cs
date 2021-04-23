using System;

namespace DA.Node
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DASerializeField : Attribute
    {
    }
}