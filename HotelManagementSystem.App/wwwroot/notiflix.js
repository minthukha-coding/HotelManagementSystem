window.notiflixNotify = {
    success: (message) => {
        Notiflix.Notify.success(message);
    },
    error: (message) => {
        Notiflix.Notify.failure(message);
    }
};

window.notiflixConfirm = {
    show: (message, dotNetObjectRef, callbackMethod) => {
        Notiflix.Confirm.show(
            'Confirm',
            message,
            'Yes',
            'No',
            function () {
                dotNetObjectRef.invokeMethodAsync(callbackMethod, true);
            },
            function () {
                dotNetObjectRef.invokeMethodAsync(callbackMethod, false);
            }
        );
    }
};
