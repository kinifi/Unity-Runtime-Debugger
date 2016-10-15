# Unity-Runtime-Debugger
Get Logs, Warnings, Errors, Full Stack Traces, FPS, and disable/enable objects at runtime to find problem GameObjects

![alt text](http://i.imgur.com/feRyDn1.png "In Editor Image")


#Setup: 

1. You can just download from Zip and take the Debugger folder and drop it into your project. 
2. Grab the Debugger Prefab and drop it into a scene. This object is marked as Do Not Destroy so you could end up with multiples of them. My Suggestion would be to place it in the splash screen. 
3. Set the button you want to activate the debugger. 
4. Use it! 

#Tree View

You can disable and enable any gameobjects in the scene EXCEPT the camera or the debugger object you will crash the game. This helps find scripts on GameObjects that are causing problems. 

#FPS View

The FPS View and the Tree view are made to go together. If you notice your FPS is down, you should disable objects until you find which one is the problem. 

#API: 

Some people have requested to toggle the debugger not by a keyboard but by anyway they want. The API for that is simple. Just GetComponent<Debugger>(); from the Debugger GameObject and call public void ToggleDebugger(); 

Example:
```csharp
private Debugger m_DebugMenu;

void Start()
{
  m_DebugMenu = GameObject.Find("Debugger").GetComponent<Debugger>();
  if(m_DebugMenu == null)
  {
    Debug.LogError("Could not Find Debugger");
  }
}

///now just call the method below in your code somewhere.
m_DebugMenu.ToggleDebugger();

```


#Tests: 

There is a test script in the project to show how it works. You can just add that and it will do logs, errors, warnings in intervals of 2 seconds. I used this as a unit test. 
