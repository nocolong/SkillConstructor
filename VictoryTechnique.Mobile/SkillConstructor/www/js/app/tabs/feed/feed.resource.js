angular.module("app.services").factory("feedResource", function (apiUrl, $http) {

    function getVidSubmissions() {
        return $http.get(apiUrl + 'vidSubmissions')
            .then(function (response) {
                return response.data;
            });
    }

    return {
        getVidSubmissions: getVidSubmissions
    };
    return $resource(apiUrl + 'vidCritiques/:vidCritiqueId', { vidCritiqueId: '@VidCritiqueId' },
    {
        'update': {
            method: 'PUT'
        },
        'vidCritiques': {
            url: apiUrl + 'vidSubmissions/:vidSubmissionId/vidCritiques',
            method: 'GET',
            isArray: true
        }
    });
});