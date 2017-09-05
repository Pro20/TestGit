(function () {
    'use strict';
    /* Service: GlobalData
     * Defines the methods related to global data across the app
     */
    btApp.factory('ContentData', function ($http, $q, BASE_URL) {
        return {
            searchArticle: function (search) {
                var deferred = $q.defer();
                $http.post(BASE_URL + 'api/searcharticle/', search).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            }
        };
    });

}());