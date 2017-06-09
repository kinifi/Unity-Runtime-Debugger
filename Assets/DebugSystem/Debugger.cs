using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Debugger : MonoBehaviour {

    public bool m_DestroyOnSceneChange = false;

	private bool m_debuggerOn = false;

	public List<Log> m_logs = new List<Log>();

	private string m_viewingStackTrace = "Select A Log/Error/Warning to view the StackTrace";
	private Vector2 scrollPosition, treeScrollPosition;

	private float deltaTime = 0.0f;

	public KeyCode m_activateKeyCode;
	
	public GUISkin m_skin;

	// Use this for initialization
	void Start () {

        //check if we want to make this object persistent throughout our scenes
        if (m_DestroyOnSceneChange == false)
        {
            DontDestroyOnLoad(this.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		//check if the user has pressed tab or not to bring up the debugger
		if(Input.GetKeyDown(m_activateKeyCode))
		{
			ToggleDebugger();
		}

		//FPS delta time 
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

	}

	public void ToggleDebugger()
	{
		m_debuggerOn = !m_debuggerOn;
    	// Debug.Log("Toggling Debugger");
	}

	private string FPSDisplay()
	{
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		return text;
	}

    void OnGUI()
    {

    	GUI.skin = m_skin;

    	//check to see if the debugger is enabled or not
    	if(m_debuggerOn == false)
    		return;


    	GUILayout.BeginArea(new Rect(Screen.width-Screen.width/4,0, Screen.width/4, Screen.height));

    	//title 
    	GUILayout.Label("Tree View of Scene - GameObjects");

    	treeScrollPosition = GUILayout.BeginScrollView(treeScrollPosition);

    	TreeCreation();

    	GUILayout.EndScrollView();

		GUILayout.EndArea();



    	//makes a background for the debugger. Passing a blank string is so it doesn't display text
		GUILayout.Box("", GUILayout.Width(Screen.width/4*3), GUILayout.Height(Screen.height));

		//create a container for the debugger
    	GUILayout.BeginArea(new Rect(0,0, Screen.width/4*3, Screen.height));

    	GUILayout.Button(Application.productName + " | Tab = Open/Close Debugger | " + " FPS: " + FPSDisplay());

    	if(m_logs.Count != 0)
    	{

	    	GUILayout.BeginVertical();

	    	scrollPosition = GUILayout.BeginScrollView(scrollPosition);

	    	
		    	for (int i = 1; i <= m_logs.Count; i++)
		        {

		        	string colorStartString;
		        	//check what log type it is so we can assign a color to the log
		        	if(m_logs[i-1].type.ToString().ToLower() == "warning")
		        	{
		        		colorStartString = "yellow";
		        	}
		        	else if(m_logs[i-1].type.ToString().ToLower() == "log")
		        	{
		        		colorStartString = "white";
		        	}
		        	else
		        	{
		        		colorStartString = "red";
		        	}
		        	
		        	//put the log into a single string
		            string fullLog = "<color=" + colorStartString + ">" + m_logs[i-1].type.ToString() + "</color>" + " : " + m_logs[i-1].log;
		            
		            //display the log inside a button so we can select it and see the full stack trace
		            if(GUILayout.Button(fullLog))
		            {
		            	m_viewingStackTrace = m_logs[i-1].stackTrace;
		            }

		        }
	    	

	        GUILayout.EndScrollView();

	        GUILayout.TextArea(m_viewingStackTrace);

	        GUILayout.EndVertical();

    	}
    	else
    	{
    		GUILayout.Label("No Logs To Display");
    	}

    	GUILayout.EndArea();
    }


    void OnEnable() 
    {
        Application.logMessageReceived += HandleLog;
    }
    
    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    
    void HandleLog(string logString, string stackTrace, LogType type) {
        
        Log newLog = new Log();
        newLog.log = logString;
        newLog.stackTrace = stackTrace;
        newLog.type = type;
        m_logs.Add(newLog);

    }


    private void TreeCreation()
    {
    	//find all the GameObjects in the scene
		foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
		{
		    	if (obj.transform.root.gameObject.activeInHierarchy)
                		GUI.color = Color.white;
           		else
                		GUI.color = Color.red;

			//TODO: check if child count exists
			if (obj.transform.childCount != 0)
			{
				// Traverse(obj);
				if(GUILayout.Button(obj.name + " - P"))
				{
					if(obj.activeSelf == true)
					{
						obj.SetActive(false);
					}
					else
					{
						obj.SetActive(true);
					}
				}
			}
			else if(obj.transform.parent != null)
			{
				if(GUILayout.Button(">" + obj.name + " - C"))
				{
					if(obj.activeSelf == true)
					{
						obj.SetActive(false);
					}
					else
					{
						obj.SetActive(true);
					}
				}
			}
			else if(obj.transform.parent == null)
			{
				if(GUILayout.Button(obj.name))
				{
					if(obj.activeSelf == true)
					{
						obj.SetActive(false);
					}
					else
					{
						obj.SetActive(true);
					}
				}
			}
		}
    }

	private void Traverse(GameObject obj)
	{

		foreach (Transform child in obj.transform) 
		{

			if(GUILayout.Button(">>>>>>" + obj.name + " - " + obj.activeSelf))
			{
				if(obj.activeSelf == true)
				{
					obj.SetActive(false);
				}
				else
				{
					obj.SetActive(true);
				}
			}

			Traverse (child.gameObject);
		}

	}

}
