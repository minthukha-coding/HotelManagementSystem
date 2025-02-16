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

window.notiflixLoadingPulse = {
    show: () => {
        Notiflix.Loading.pulse();
    }
};

window.notiflixLoadingRemove = {
    show: () => {
        Notiflix.Loading.remove();
    }
};

window.manageLoading = (action) => {
    if (action === 'show') {
        Notiflix.Loading.circle(); // or any other Notiflix loading method
    } else if (action === 'remove') {
        Notiflix.Loading.remove();
    }
};