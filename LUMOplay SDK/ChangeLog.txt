LUMOplay Unity SDK

Version 1.0.1 - Release
	- Fixed bug in communication with the LUMOplay software that would sometimes send the ready signal too early causing the game-switch overlay to be removed too soon

Version 1.0.0 - Release
	- OnMotionUI fix implemented to correctly convert UI space to screen pixel space

Version 1.0.0 - Beta 9
	- Added InitializeCommunication component that can be added in games that don't have motion detection (mouse, touch, controller, etc). This will allow the communications sockets to be initialized.
	- All Motion components that use Raycasts have had a configurable raycastDistance added, it defaults to Mathf.Infinity
		- Shorter Raycast distances can improve performance, but the distance must be far enough to reach the collider from the camera (where the Raycast will originate)
	- Added tooltips and more robust commenting to attachable Motion components (OnMotion2D, Particles on Movement 3D, etc)
	- Added check for a RigidBody in DirectionForceOnMovement before attempting to apply forces
	- ExplosionForceOnMovement changed to FixedUpdate from Update (as motion events only occur on FixedUpdate)
	- ParticlesOnMovementXD components have had an additional field added to allow for different axes to be selected for sprite sorting correction (or none if desired)
	- Assembly Definitions were added for LUMOplay SDK and LUMOplay SDK Examples, this will prevent this code from being recompiled unless changes were made within these sections
	- Better commenting added for LUMOplay SDK Example scripts
	- Added better commenting to LUMOplay SDK/utils classes
