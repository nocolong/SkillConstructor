angular.module('app.services').factory('disciplineResource', function (apiUrl, $resource) {
    return $resource(apiUrl + 'discipline/:disciplineId', { topicId: '@AreaOfStudyId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});