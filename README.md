# Unity Car Demo
*** This is not a real game project. ***

![My Image](ReadmeImages/MainMenuScene.jpg)

![My Image](ReadmeImages/CarDemoScene_Stage01.jpg)

# Usage
Unity Editor Version Required:
```bash
Unity 6 (6000.0.44f1)
```

Starting Scene:
```bash
Assets/CarDemo/Scenes/MenuScene
```

# Code Structure
![My Image](ReadmeImages/GeneralCodeStructure.jpg)

A `GameDirector` is created for accessing all major features:
- `GameInputManager` (abstraction for inputs like keyboard and touch screen)
- `CameraManager` (camera movement control)
- `ObjectTagManager` (object tagging system for accessing objects quickly)
- `GameEventManager` (game event mechanism)
- `GameConditionManager` (system to save game conditions)
- `StateMachineManager` (state machine system for controlling game flow)
- `GameLoadingManager` (scene loading management)
