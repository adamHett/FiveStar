var FSD = angular.module('FSD').controller('MentalHealthPortalViewController', function ($scope, $http) {


    //Function returns all Applicant/Resident Mental Health Assessments located in database
    $http({
        method: 'GET',
        url: '/FiveStarApplicantInterviewInformation/GetAllMentalHealthProfiles',
        params: {}
    }).then(function success(result) {
        $scope.AMHList = result.data;
    });

})