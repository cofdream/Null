using UnityEngine;

namespace DA.AssetsBundle
{
    public class Resource
    {
        public string Name { get => Data.name; }
        public UnityEngine.Object Data { get; private set; }

        
    }

    public class ResourceLoad
    {

    }
}