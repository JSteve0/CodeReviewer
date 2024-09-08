# Code Reviewer

## Author(s)

- [Justin Stevens](https://github.com/JSteve0/)

## Project Description

Code Reviewer is a text editor currently in development that integrates advanced AI features for enhanced coding assistance. The application aims to support multiple programming languages file creation, editing, and validation.

## Table of Contents
- [Code Reviewer](#code-reviewer)
  - [Author(s)](#authors)
  - [Project Description](#project-description)
  - [Table of Contents](#table-of-contents)
  - [Current Features](#current-features)
  - [Future Features](#future-features)
  - [Project Images](#project-images)
  - [User Guide](#user-guide)
  - [Developing and Contributing](#developing-and-contributing)
    - [License](#license)


## Current Features

- **File Management**: Create, open, edit, and save C#, Java, JavaScript, and TypeScript files.
- **Editor**: Utilizes the [Monaco Editor](https://microsoft.github.io/monaco-editor/) with:
  - Basic syntax colorization
  - IntelliSense and code validation
- See all features and changes [here](./changelog.md)

## Future Features

- AI-powered code review and suggestions
- Enhanced file management capabilities
- Support for additional programming languages

## Project Images

![App Image](./ReadMeImages/AppImage.png)

## User Guide
Please see the [User Guide](./UserGuide.md) for installation and usage information

## Developing and Contributing
- Follow the guide in the latest release

1. **Clone the repository**:
    ```bash
    git clone https://github.com/JSteve0/CodeReviewer.git
    ```
2. **Download and install the code editor**:
    1. Download [Monaco-Editor Version 0.5](https://registry.npmjs.org/monaco-editor/-/monaco-editor-0.50.0.tgz) Zip from this link.
   1. Extract the contents 
   2. Move the `package/min` folder in `CodeReviewer/MonacoEditor`
3. **Run the application**:
  - Use an IDE like Visual Studio or JetBrains Rider to run the project.
  - I'll list alternative ways to run the application here in the future.

### License

This project is licensed under the [MIT License](./LICENSE).
