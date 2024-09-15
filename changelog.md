# Changelog

## Next Release: Version (0.2.0) - (TBD)

### **Features**
- Added keyboard shortcuts!
  - Menu tabs now display the relevant shortcut if one exists
  - Current shortcuts:
    - New file: Ctrl+N
    - Save file: Ctrl+S
    - Open file: Ctrl+O
    - Open new window: Ctrl+Shift+N
    - Toggle Fullscreen: F11
- Added a 'Window' tab to the menu bar
  - Contains the 'New window' button
  - Contains the new 'Fullscreen' button which toggles fullscreen mode, use the F11 key as a shortcut
  - Will contain additional window-related commands in the future
- Added a 'Help' tab to the menu bar
  - Contains the new 'About' button which displays the application information
  - Contains the new 'GitHub' button which opens the GitHub repository in a web browser
  - Will contain additional help-related commands in the future
- Added a new 'About' dialog
  - Displays the application name, version, and release date
  - Contains a brief description of the application
  - Provides links to the GitHub repository and the developer's website
- Added new programming language support:
  - C++

### **Changes**
- Moved the 'New window' button from the 'File' tab to the new 'Window' tab

### **Bug Fixes**
- Updated scrollbar color to match the dark mode theme.

## Version 0.1.0 - Initial Release (9/8/2024)

### Features
- **Monaco Editor Integration**:
  - Implemented the [Monaco Editor](https://microsoft.github.io/monaco-editor/).
  - Supports basic syntax highlighting.
  - Includes IntelliSense functionality and code validation for supported languages.

- **File Handling**:
  - Added support for opening any file directly within the editor.
  - Enabled saving files that are opened from the local drive.

- **File Creation**:
  - Introduced a command to create new files for supported programming languages.
  - Automatically inserts starter code when creating new files.

- **Programming Language Support**:
  - Added support for the following languages:
    - C#
    - Java
    - JavaScript
    - TypeScript

- **Multi-Window Support**:
  - Added a "New Window" command, allowing multiple editor instances to be opened simultaneously in separate windows.

- **User Interface**:
  - Implemented full-screen mode and dynamic window resizing.
  - Added an information panel at the bottom of the editor, displaying:
    - The current programming language, if detected.
    - The name and path of the currently opened file, if applicable.

- **Commands**:
  - Added an "Exit" command to close the current window.
