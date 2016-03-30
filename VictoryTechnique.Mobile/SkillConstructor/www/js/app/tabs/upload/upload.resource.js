angular.module('app').factory('uploadResource', function (apiUrl, $resource) {
    return $resource(apiUrl + 'VidSubmissions/:uploadId', { uploadId: '@VidSubmissionId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});