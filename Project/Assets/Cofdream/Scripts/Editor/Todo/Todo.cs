using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CofdreamEditor.Core
{
    public enum TodoStateType : byte
    {
        ToDo = 0,
        Doing,
        Done,
    }

    [Serializable]
    public class Todo
    {
        public TodoStateType StateType;
        [TextArea] public string Content;
    } 
}