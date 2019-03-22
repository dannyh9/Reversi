let Ajax = (function () {
    function sendGetRequest(url, onSuccess, onError) {
        return sendRequest(url, null, 'GET', onSuccess, onError);
    }

    function sendPatchRequest(url, data, onSuccess, onError) {
        return sendRequest(url, data, 'PATCH', onSuccess, onError);
    }

    function sendPutRequest(url, data, onSuccess, onError) {
        return sendRequest(url, JSON.stringify(data), 'PUT', onSuccess, onError);
    }

    function sendPostRequest(url, data, onSuccess, onError) {
        return sendRequest(url, JSON.stringify(data), 'POST', onSuccess, onError);
    }

    function sendDeleteRequest(url, onSuccess, onError) {
        return sendRequest(url, null, 'DELETE', onSuccess, onError);
    }

    function sendRequest(url, data, verb, onSuccess, onError) {
        var request = {
            url: url,
            type: verb,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: data,
            success: onSuccess,
            error: onError
        };
        return $.ajax(request);
    }

    return {
        sendPostRequest: sendPostRequest,
        sendGetRequest: sendGetRequest
    };
})();
