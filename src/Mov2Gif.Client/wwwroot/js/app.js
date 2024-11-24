window.downloadFile = function (url) {
    const a = document.createElement('a');
    a.href = url;
    a.download = 'converted.gif';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};

window.getDroppedFiles = async function (dataTransfer) {
    const files = [];
    if (dataTransfer.items) {
        for (let i = 0; i < dataTransfer.items.length; i++) {
            if (dataTransfer.items[i].kind === 'file') {
                const file = dataTransfer.items[i].getAsFile();
                files.push({
                    name: file.name,
                    size: file.size,
                    type: file.type,
                    lastModified: file.lastModified
                });
            }
        }
    }
    return files;
};

window.preventDefaultDragOver = function (event) {
    event.preventDefault();
};