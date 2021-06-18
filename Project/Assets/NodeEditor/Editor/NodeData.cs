using UnityEngine;
using UnityEngine.UI;

namespace DA.Node
{
    public class NodeData
    {
        public Person person;

        public void Draw()
        {
            person.Name = GUILayout.TextField(person.Name);

            string value = GUILayout.TextField(person.Age.ToString());

            int.TryParse(value, out person.Age);
            //GUILayout.Label(person.Friend);
        }
    }
}