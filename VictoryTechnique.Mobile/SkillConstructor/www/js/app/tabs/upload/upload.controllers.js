angular.module("app").controller("uploadCtrl", function ($scope, uploadResource, disciplineResource) {

    function activate() {
        $scope.upload = uploadResource.query();
        $scope.discipline = disciplineResource.query();
    }

    $scope.chooseDiscipline = function (discipline) {
        $scope.AreaOfStudy = discipline;
        $scope.AreaOfStudyId = discipline.AreaOfStudyId;
        $scope.newVidSubmission.AreaOfStudyId = discipline.AreaOfStudyId;
    }

    $scope.deleteVidSubmission = function (upload) {
        upload.$remove(function () {
            alert('Question Removed');
            activate();
        });
    };

    $scope.createVidSubmission = function () {
        uploadResource.save($scope.newVidSubmission, function () {
            $scope.newVidSubmission = {};
            $scope.AreaOfStudy = {};
        });
    };

    activate();

});