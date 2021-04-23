using System;

namespace DA.Node
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DASerializeClass : Attribute
    {
    }
}