window.downloadFile = function (url) {
    const a = document.createElement('a');
    a.href = url;
    a.download = 'converted.gif';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};

window.getDroppedFiles = function (dataTransfer) {
    return Array.from(dataTransfer.files).map(file => {
        return {
            name: file.name,
            size: file.size,
            type: file.type,
            lastModified: file.lastModified
        };
    });
};