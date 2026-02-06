## Simple Interaction System

**[Notice: This asset uses Unity's NEW Input System. Make sure it is installed and enabled in your project.]**

### How to use
1. Go into the Samples folder and drag the PopupManager prefab into your scene.
   Then assign your preferred Interact Button prefab in the PopupManager inspector.
2. Add the Interactor (2D or 3D) component to your player
3. Add a Collider (2D or 3D) and the Interactable component to your objects.
4. Press play and tweak settings to your liking.

### How to customize
The system works out of the box with no code required. Customization is optional.

You can derive from any of the scripts to create custom version that suite your needs.
To ditch the popup manager for your own, you can create a custom Interactor2D or 3D 
and override the `void OnNearestInteractableChanged(Interactable interactable)`
function to enable your own popup manager. 
In fact, here is a list of overridable functions for each script:

- Interactor2D.cs & Interactor3D.cs
  - `void OnNearestInteractableChanged(Interactable interactable)`
  - `void UpdateNearestInteractable()`

- Interactable.cs
  - `void Interact()`

- PopupManager.cs
  - `void ChangeInteractableObject(Interactable interactable)`
  - `GameObject CreateInteractButton(Interactable interactable)`
  - `void AnimateIn()`
  - `void AnimateOut()`

Example Code:

```aiignore
public class CustomInteractable : Interactable {
    [SerializeField] private string myCustomString = "Test..";
    
    public override void Interact()
    {
        // Write your custom code here
        
        Debug.Log(myCustomString); // Test custom code
        
        base.Interact();
    }
}
```