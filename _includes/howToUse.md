## How to Use

### Controls
The main controls are as follows:

* Trigger: Used to pick up objects. Releasing the trigger will drop the object from the users hand. If the object is close enough to its final location, the object will snap in place.

* Grip: Brings up Interactable UI. Each of the 4 blocks have their function written above them. Releasing the grip while the controller is inside the blocks will activate them.

* Touchpad: Clicking the touchpad and aiming at a teleportable surface (i.e. the floor) will spawn a reticle at that location. Releasing the touchpad will teleport the user to that location.

### Using the Package Release

As stated above, the release only displays the functionality and does not allow for the importing of assets. Upon running the executable, the user is placed within the VR environment with the preloaded asset. The user can then freely navigate around the area with teleportation and body movement given sufficient room space.

The user can then proceed assemble the parts manually or choose to autoassemble the parts. The parts will snap into place when released in a close proximity to the final location of the part that is indicated by the blue highlighted ghost. At any point while holding a part, the user may look at the display that is in the room to view the information attached to that part.

Upon finishing an assembly, the user can then choose to either reset the scene, reset and then autobuild, or exit. These options are accessed by holding down the grip buttons and then moving the controller into the desired option and releasing the grip. The exit functionality quits the entire application.

### Importing and Building

In order to use the Unity extension included, the .zip file must be downloaded, unpacked and open up the project folder in the Unity editor.

At this point, add the desired .fbx object to the `/assets/` folder of the Unity project. Then move the desired assembly into the game environment end ensure it is within the play area. Click on the assembly and navigate to the `Window` and then `Annotate Object Metadata`.

In the new window, set the entire assembly as root. Either generate the assembly order through automatic generation which orders them based on their order in the list or manually create the order by adding `order: x;` to each tag where x is a number specifying the order it should be assembled in.

__Note:__ _Multiple parts may share the same order number, this allows them to be assembled interchangably and the next part will now highlight until all parts of that order level have been placed._

Any other metadata desired to be attached to the part (e.g. length, width, height, material, reference information) can be added by providing a another meta data entry such as `length: 52;`. This information will be displayed on the in game screen when the part is held.

Once satisfied with the tags and ordering, the simulation can be started by pressing the play button in the Unity editor. From this point onward, the process is the same as if the the executable was run.

The Annotate Object Metadata windows also allows for all of the tags to easily be cleared by clicking on `Clear All Tags`. If an individual part is selected, it can be determined if the part is next in the build sequence by clicking `Check If This Item Is Next`.

__Note:__ _Individual parts of an assembly can be modified in this window as well by selecting on the object while the window is still up or by opening the window while the part is currently selected_