# Build instructions
The required Lenovo DLLs are not included for copyright reasons. You will need to alter the hint path in the .csproj file for the `Keyboard_Core.dll` reference. On my machine, the DLL resides in `C:\ProgramData\Lenovo\ImController\Plugins\ThinkKeyboardPlugin\x86`.

The project targets .NET 4.8 so you will need the .NET 4.8 Targetting Pack installed, or you can downgrade the target to an older framework.
