// Version 1.1

const API_URL = 'http://127.0.0.1:8000';

let currentLanguage = '';
let currentText = '';
let editor = null;

document.addEventListener('DOMContentLoaded', () => {
    initializeWebView();
    setupEventListeners();
    setupIntervals();
    initializeEditor('javascript', 'function helloWorld() {\n\tconsole.log("Hello world!");\n}\n');
});

function initializeWebView() {
    navigator.sendBeacon(`${API_URL}/connect`, JSON.stringify({
        message: `New client connected${window.chrome.webview ? ' from WebView2' : ''}`
    }));
}

function setupEventListeners() {
    if (window.chrome.webview) {
        window.chrome.webview.addEventListener('message', handleWebViewMessage);
    }

    window.addEventListener('beforeunload', () => {
        navigator.sendBeacon(`${API_URL}/disconnect`, JSON.stringify({
            message: 'Client disconnected'
        }));
    });
}

function setupIntervals() {
    if (window.chrome.webview) {
        // Monitor for text changes
        setInterval(() => {
            if (editor && currentText !== editor.getValue()) {
                let message = {editor_text: editor.getValue()};
                console.log('Sending Message:', message);
                window.chrome.webview.postMessage(message);
                
                currentText = editor.getValue();
            }
        },100)
    }
}

function handleWebViewMessage(event) {
    console.log("Message Received:", event, event.data);

    const { language, action } = event.data;

    if (language) {
        handleLanguageChange(language);
    }

    if (action) {
        handleAction(action);
    }
}

function handleLanguageChange(language) {
    if (currentLanguage === language) return; // No change in language

    disposeEditor();
    const defaultContent = language === 'csharp' ?
        'using System;\n\nclass Sample {\n\n    public static void Main(String[] args) {\n        Console.WriteLine("Hello world!");\n    }\n\n}\n' :
        'function helloWorld() {\n\tconsole.log("Hello world!");\n}\n';

    initializeEditor(language, defaultContent);
}

function handleAction(action) {
    if (action === 'clear') {
        disposeEditor();
        initializeEditor(currentLanguage, '');
        console.log('Editor cleared');
    }
}

function initializeEditor(language, value) {
    editor = monaco.editor.create(document.getElementById('container'), {
        value: value,
        language: language,
        automaticLayout: true
    });
    currentLanguage = language;
}

function disposeEditor() {
    if (editor) {
        editor.dispose();
        editor = null;
    }
}
