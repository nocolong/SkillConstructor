angular.module('app.controllers').controller('AppController', function ($scope, AuthenticationService) {
    $scope.logOut = function () {
        AuthenticationService.logout().then(
            function () {
                location.replace('/#/home')
            });
    };
});