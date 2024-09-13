# Changelog

## Next Release: Version (0.1.1) - (TBD)

### **Features**
- Added keyboard shortcuts!
  - New file: Ctrl+N
  - Open new window: Ctrl+Shift+N
  - Save file: Ctrl+S
  - Open file: Ctrl+O
- Menu tabs now display the relevant shortcut if one exists
- Added fullscreen command to the 'Window' tab
  - Use F11 as a shortcut

### **Changes**
- Added 'Window' tab and move the new window button to that tab

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
