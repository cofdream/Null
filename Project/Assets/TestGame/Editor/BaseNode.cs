using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseNode : Node
{
    public string nodeGuid;

    public BaseNode()
    {
        styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/TestGame/Editor/StyleSheet/Node.uss"));
        AddToClassList("Node");
    }

    protected void AddOutoputProt(string name)
    {
        var outputProt = GetPortInstance(Direction.Output);
        outputProt.portName = name;
        outputContainer.Add(outputProt);
    }

    protected void AddInputProt(string name)
    {
        var inputProt = GetPortInstance(Direction.Input);
        inputProt.portName = name;
        inputContainer.Add(inputProt);
    }


    private Port GetPortInstance(Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
    }
}


public class DialogueGraph : EditorWindow
{

    public Dialogue currentDialogue { get; set; }
    DialogueGrapView graphView;


    [OnOpenAsset(1)]
    public static bool ShowWindow(int instanceId, int line)
    {
        var item = EditorUtility.InstanceIDToObject(instanceId);
        if (item is Dialogue)
        {
            var window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent("Dialogue Graph");
            window.minSize = new Vector2(500, 250);

            return true;
        }
        return false;
    }

    private void OnEnable()
    {
        ConstructGrapView();
        GenerateToolbar();
    }
    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }


    void ConstructGrapView()
    {
        graphView = new DialogueGrapView { name = "Dialogue Grapg" };
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }
    void GenerateToolbar()
    {
        var toolbar = new Toolbar();
        toolbar.Add(new ToolbarButton(() => { }) { text = "Save" });

        rootVisualElement.Add(toolbar);
    }
}

public class DialogueGrapView : GraphView
{
    public DialogueGrapView()
    {
        styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/TestGame/Editor/StyleSheet/MarrativeGraph.uss"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);


        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new FreehandSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
    }
}
