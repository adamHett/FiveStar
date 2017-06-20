var FSD = angular.module('FSD').controller('FrontDeskPortalViewController', function ($scope, $http) {


    //Function returns applicants with incomplete applications
    $http({
        method: 'GET',
        url: '/FiveStarApplicantInformation/GetIncompleteApplicantProfiles',
        params: {}
    }).then(function success(result) {
        $scope.IAList = result.data;
    });

    //Function returns all applicants located in database
    $http({
        method: 'GET',
        url: '/FiveStarApplicantInformation/GetAllApplicantProfiles',
        params: {}
    }).then(function success(result) {
        $scope.AAList = result.data;
    });

})
