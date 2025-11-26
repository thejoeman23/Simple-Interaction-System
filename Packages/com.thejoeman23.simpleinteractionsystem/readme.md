## Simple Interaction System

**[Notice: This uses Unity's new Input System]**

### How to use
1. Go into the Samples folder and drag the PopupManager prefab into your scene, and set the Interact Button Prefab to your prefered prefab
2. Select the player asset
3. Add the Interactor (2D or 3D) component and tweak variables
4. Select interactable object
5. Give it a collider
6. Add the Interactable component and tweak variables to your liking
7. Press play and try it out!

### How to customize
You can derive from any of the scripts to create custom version that suite your needs.
To ditch the popup manager for your own, you can create a custom Interactor2D or 3D 
and override the `void OnNearestInteractableChanged(Interactable interactable)`
function to enable your own popup manager. 
In fact, here is a list of overridable functions for each script:

- Interactor.cs
  - `void OnNearestInteractableChanged(Interactable interactable)`
  - `void UpdateNearestInteractable()`

- Interactable.cs
  - `void Interact()`

- PopupManager.cs
  - `void ChangeInteractableObject(Interactable interactable)`
  - `GameObject CreateInteractButton(Interactable interactable)`
  - `void AnimateIn()`
  - `void AnimateOut()`

