angular.module('app').factory('myVideosResource', function (apiUrl, $http) {

    function getCurrentUser() {
        return $http.get(apiUrl + 'accounts/currentuser')
                    .then(function (response) {
                        return response.data;
                    });
    }
    function getUserVidSubmissions() {
        return $http.get(apiUrl + 'vidSubmissions/user')
                    .then(function (response) {
                        return response.data;
                    });
    }
    return {
        getCurrentUser: getCurrentUser,
        getUserVidSubmissions: getUserVidSubmissions
    };

});
