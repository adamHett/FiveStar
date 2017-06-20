var FSD = angular.module('FSD').controller('ResidentPortalViewController', function ($scope, $http) {


    //Function returns all residents located in database
    $http({
        method: 'GET',
        url: '/FiveStarResidentInformation/GetAllResidentProfiles',
        params: {}
    }).then(function success(result) {
        $scope.ARList = result.data;
    });

   


})