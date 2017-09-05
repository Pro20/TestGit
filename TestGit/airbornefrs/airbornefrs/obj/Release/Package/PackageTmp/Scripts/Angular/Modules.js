/// <reference path="../angular.js" /> 
/// <reference path="../angular.min.js" />   
/// <reference path="../angular-animate.js" />   
/// <reference path="../angular-animate.min.js" />   
/// <reference path="../angular-file-upload.js" />   
/// <reference path="../angular-file-upload.min.js" />   
var app;
var shoppingCartList = [{ Item_IDs: '', Item_Names: '', Item_Prices: '', Image_Names: '', Descriptions: '', ItemCount: 0 }];

(function () {
    app = angular.module("RESTClientModule", ['ngMaterial', 'material.svgAssetsCache', 'ui.bootstrap']);
})();




app.filter('cut', function () {
    return function (value, wordwise, max, tail) {
        if (!value) return '';

        max = parseInt(max, 10);
        if (!max) return value;
        if (value.length <= max) return value;

        value = value.substr(0, max);
        if (wordwise) {
            var lastspace = value.lastIndexOf(' ');
            if (lastspace != -1) {
                value = value.substr(0, lastspace);
            }
        }
        return value + (tail || ' …');
    };
});