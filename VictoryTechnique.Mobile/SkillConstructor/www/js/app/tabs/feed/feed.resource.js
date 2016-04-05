angular.module("app.services").factory("feedResource", function (apiUrl, $http, $resource) {

    function getVidSubmissions() {
        return $http.get(apiUrl + 'vidSubmissions')
                    .then(function (response) {
                        return response.data;
                    });
    }
    function getCritiquesForSubmissions(submissionId) {
        return $http.get(apiUrl + 'vidSubmissions/' + submissionId + '/vidCritiques')
                    .then(function (response) {
                        return response;
                    });
    }

    return {
        resource: $resource(apiUrl + 'vidCritiques/:vidCritiqueId', { vidCritiqueId: '@VidCritiqueId' },
        {
            'update': {
                method: 'PUT'
            }
        }),
        getVidSubmissions: getVidSubmissions,
        getCritiquesForSubmissions: getCritiquesForSubmissions
    };
    
});

