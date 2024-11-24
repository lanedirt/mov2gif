export function initializeFileDropZone(dropZoneElement, inputElement, dotnetHelper) {
    // Prevent default drag behaviors
    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        dropZoneElement.addEventListener(eventName, preventDefaults, false);
        document.body.addEventListener(eventName, preventDefaults, false);
    });

    // Handle drag enter/leave
    dropZoneElement.addEventListener('dragenter', () => {
        dotnetHelper.invokeMethodAsync('OnDragEnter');
    });

    dropZoneElement.addEventListener('dragleave', (e) => {
        // Only trigger if actually leaving the container (not entering a child element)
        if (e.target === dropZoneElement) {
            dotnetHelper.invokeMethodAsync('OnDragLeave');
        }
    });

    // Handle drops
    dropZoneElement.addEventListener('drop', e => {
        dotnetHelper.invokeMethodAsync('OnDragLeave');
        const dt = e.dataTransfer;
        const files = dt.files;
        inputElement.files = files;

        const event = new Event('change', { bubbles: true });
        inputElement.dispatchEvent(event);
    });

    function preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }

    return {
        dispose: () => {
            ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
                dropZoneElement.removeEventListener(eventName, preventDefaults);
                document.body.removeEventListener(eventName, preventDefaults);
            });
        }
    };
}