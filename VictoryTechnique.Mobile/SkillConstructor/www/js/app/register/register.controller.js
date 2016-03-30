angular.module("app").controller("registerCtrl", function ($scope, $state, AuthenticationService, $timeout) {

    $scope.registration = {};

    $scope.register = function () {
        AuthenticationService.register($scope.registration).then(
            function (response) {
                alert("Registration complete.");
                

                $scope.register = "Stand by.. ";

                $timeout(function () {
                    AuthenticationService.login({ username: $scope.registration.UserName, password: $scope.registration.Password }).then(
                        function (response) {
                            $state.go('tabsController.feed');
                        },
                        function (err) {
                            alert(err.error_description);
                        }
                    );
                }, 1000);

            },
            function (error) {
                alert("Failed to register");
            }
        )
    };

});