window.downloadFile = function (url) {
    const a = document.createElement('a');
    a.href = url;
    a.download = 'converted.gif';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};

window.getDroppedFiles = function (dataTransfer) {
    const files = dataTransfer.files;
    return files;
};