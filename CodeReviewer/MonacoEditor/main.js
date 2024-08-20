// Version 1.0

let currentLanguage = '';

let editor = null;

setTimeout(
    () => navigator.sendBeacon('http://127.0.0.1:8000/connect', 
        JSON.stringify({ 
            message: 'New client connected' + ((window.chrome.webview !== null && window.chrome.webview !== undefined) ? ' from WebView2' : '')
        })
    ),
    100
)

function dispose() {
    if (editor !== null) {
        editor.dispose();
    }
}
      
if (window.chrome.webview !== null && window.chrome.webview !== undefined) {
    window.chrome.webview.addEventListener('message', arg => {
        console.log("Message Received" + arg, arg.data);

        if (arg.data.language !== undefined && arg.data.language !== null) {
            if (arg.data.language === 'csharp') {
                dispose();
                editor = monaco.editor.create(document.getElementById('container'), {
                    value: 'using System;\n' +
                        '\n' +
                        'class Sample {\n' +
                        '\n' +
                        '    public static void Main(String[] args) {\n' +
                        '        Console.WriteLine("Hello world!");\n' +
                        '    }\n' +
                        '\n' +
                        '}\n',
                    language: 'csharp',
                    automaticLayout: true
                });
                currentLanguage = 'csharp';
            } else if (arg.data.language === 'javascript') {
                dispose();
                editor = monaco.editor.create(document.getElementById('container'), {
                    value: 'function helloWorld() {\n\tconsole.log("Hello world!");\n}\n',
                    language: 'javascript',
                    automaticLayout: true
                });
                currentLanguage = 'javascript';
            }

            console.log(currentLanguage);
        }

        if (arg.data.action !== undefined && arg.data.action !== null) {
            if (arg.data.action === 'clear') {
                dispose();
                editor = monaco.editor.create(document.getElementById('container'), {
                    value: '',
                    language: currentLanguage,
                    automaticLayout: true
                });
            }
            console.log('clear')
        }
    });
} else {
    editor = monaco.editor.create(document.getElementById('container'), {
        value: 'function helloWorld() {\n\tconsole.log("Hello world!");\n}\n',
        language: 'javascript',
        automaticLayout: true
    });
    currentLanguage = 'javascript';
}

window.addEventListener('beforeunload', (event) => {
    // Sending a synchronous POST request to notify the server
    navigator.sendBeacon('http://127.0.0.1:8000/disconnect', JSON.stringify({ message: 'Client disconnected' }));
});
