(function () {
    'use strict';
    /* Service: GlobalData
     * Defines the methods related to global data across the app
     */
    btApp.factory('ApplyData', function ($http, $q, BASE_URL) {
        return {
            testMethod: function (search) {
                var deferred = $q.defer();
                $http.post(BASE_URL + 'api/testing', search).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            }
        };
    });

});