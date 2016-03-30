angular.module("app").controller("loginCtrl", function ($scope, $state, AuthenticationService) {

    $scope.loginData = {};

    $scope.login = function () {
        AuthenticationService.login($scope.loginData).then(
            function (response) {
                $state.go('tabsController.feed');
            },
            function (err) {
                alert(err.error_description);
            }
        );
    };

    $scope.logout = function () {
        AuthenticationService.logout();
    }

});