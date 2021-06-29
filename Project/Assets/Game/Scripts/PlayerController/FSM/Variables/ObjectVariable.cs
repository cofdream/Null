using System;

namespace Game
{
    [Serializable]
    public class ObjectVariable : DAScriptableObject
    {
        public string Name;
        public object Value;
    }
}