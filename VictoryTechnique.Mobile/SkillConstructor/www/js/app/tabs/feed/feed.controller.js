angular.module("app").controller("feedCtrl", function ($scope, $http, feedResource) {

    function activate() {
        
        $scope.vidSubmissions = feedResource.getVidSubmissions();
    }

    $scope.addVidCritique = function (vidSubmission) {
        vidSubmission.newVidCritique.VidSubmissionId = vidSubmission.VidSubmissionId;
        feedResource.save(vidSubmission.newVidCritique, function () {
            alert('Critique saved');
            activate();
        });
    };

    activate();
});