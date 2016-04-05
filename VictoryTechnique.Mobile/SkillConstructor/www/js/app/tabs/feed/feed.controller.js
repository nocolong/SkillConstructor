angular.module("app").controller("feedCtrl", function ($scope, $http, feedResource) {

    function activate() {
        feedResource.getVidSubmissions().then(function (submissions) {
            $scope.vidSubmissions = submissions;
        });
    }

    $scope.addVidCritique = function (vidSubmission) {
        vidSubmission.newVidCritique.VidSubmissionId = vidSubmission.VidSubmissionId;
        feedResource.resource.save(vidSubmission.newVidCritique, function () {
            alert('Critique saved');
            activate();
        });
    };

    activate();
});