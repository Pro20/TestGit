

angular
    .module('MyApp', [
    'ngRoute',
    'Anics.FrontEnd',
    ])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        $routeProvider.when('/', {
            templateUrl: '/Home/CRUD',
            controller: 'loginController'
        });
        $routeProvider.when('/login', {
            templateUrl: '/Home/loginPage',
            controller: 'crudController'
        });
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });

    }]);