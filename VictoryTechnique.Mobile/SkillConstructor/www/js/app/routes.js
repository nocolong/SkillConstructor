angular.module('app.routes', [])

.config(function ($stateProvider, $urlRouterProvider) {

    // Ionic uses AngularUI Router which uses the concept of states
    // Learn more here: https://github.com/angular-ui/ui-router
    // Set up the various states which the app can be in.
    // Each state's controller can be found in controllers.js
    $stateProvider
    .state('tabsController.submitAVideo', {
        url: '/submit',
        views: {
            'tab2': {
                templateUrl: 'templates/app/submitAVideo/submitAVideo.html',
                controller: 'uploadCtrl'
            }
        }
    })

    .state('tabsController.myVideos', {
        url: '/myVideos',
        views: {
            'tab3': {
                templateUrl: 'templates/app/myVideos/myVideos.html',
                controller: 'myVideosCtrl'
            }
        }
    })

    .state('tabsController', {
        url: '/page1',
        templateUrl: 'templates/app/tabsController/tabsController.html',
        abstract: true
    })

    .state('tabsController.feed', {
        url: '/feed',
        parent: 'tabsController',
        views: {
            'tab4': {
                templateUrl: 'templates/app/feed/feed.html',
                controller: 'feedCtrl'
            }
        }
    })

    .state('tabsController.feed.critique', {
        url: '/critique/:vidSubmissionId',
        views: {
            'tab4@tabsController': {
                templateUrl: 'templates/app/critiques/critiques.html',
                controller: 'critiqueCtrl'
            }
        }
    })

    .state('home', {
        url: '/home',
        templateUrl: 'templates/app/home/home.html',
        controller: 'homeCtrl'
    })

    .state('login', {
        url: '/login',
        templateUrl: 'templates/app/login/login.html',
        controller: 'loginCtrl'
    })

    .state('signup', {
        url: '/signup',
        templateUrl: 'templates/app/signup/signup.html',
        controller: 'registerCtrl'
    })

    $urlRouterProvider.otherwise('/home')



});