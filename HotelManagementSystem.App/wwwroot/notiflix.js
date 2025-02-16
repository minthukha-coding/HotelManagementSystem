window.notiflixNotify = {
    success: (message) => {
        setTimeout(() => {
            Notiflix.Notify.success(message);
        }, 1000); // 1000 milliseconds = 1 second
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
