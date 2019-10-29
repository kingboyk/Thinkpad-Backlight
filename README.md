# Lenovo files needed
This utility requires the Lenovo files `Keyboard_Core.dll` and `Contract_Keyboard.dll` which are not included in this repository or software release for copyright reasons. If the program exits saying that one of these files is not found, locate the `ThinkKeyboardPlugin` folder on your computer - which should contain both files - and edit the following setting in the app.config:

            <setting name="DllPath" serializeAs="String">
                <value>C:\ProgramData\Lenovo\ImController\Plugins\ThinkKeyboardPlugin\x86</value>
            </setting>
			
If you don't have this folder, either a) ensure you have the Lenovo utilities installed or b) panic.
			
# Build instructions
The project targets .NET 4.8 so you will need the .NET 4.8 Targetting Pack installed, or you can downgrade the target to an older framework.
