angular.module("app").controller("myVideosCtrl", function ($scope, $http, myVideosResource, AuthenticationService) {


    function activate() {
        
        $scope.currentUser = myVideosResource.getCurrentUser();
        $scope.vidSubmissions = myVideosResource.getUserVidSubmissions();
    }

    activate();
    $scope.logout = function () {
        AuthenticationService.logout();
        $state.go('home');
    }

  
});