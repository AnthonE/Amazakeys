# Amazakeys
Grab Unity input and turn it into keycodes and hotkeys.

Unity does not know what key your pressing. This helps catch the input and get the keycode.
We use buttons to fire off a method that takes a simple struct. The struct has the keycode and an Action.
Actions are used to describe what the keycode stands for. 
The Simple UI uses the struct to build text and alert the user if action is taking a new keycode.
Easy to extend but not modular really. Not for sure but tried to avoid any hot paths, if im using that correctly. 

Example use:
Input.GetKeyDown(TryActionKey(AmazaAction.Interact));
