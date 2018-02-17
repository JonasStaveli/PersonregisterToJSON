angular.module('PersonregisterToJSON', [])
    .controller('ApiTestCtrl', function ($scope, $http) {
        
        $scope.title = "Personregister JSON API";
        $scope.display = true;
        $scope.sok = "";
        $scope.NIN = "";
        $scope.fname = "";
        $scope.ename = "";
        $scope.PostalAddress = "";
        $scope.PostalCode = "";
        $scope.PostalPlace = "";
        $scope.err = "";
        $scope.hideErr = true;

        $scope.getPerson = function () {

            $http.get("/personregister/" + $scope.sok).then(function (data, status, headers, config) {
                $scope.fname = data.data.Person.GivenName;
                $scope.ename = data.data.Person.Sn;
                $scope.NIN = data.data.Person.NIN;
                $scope.PostalAddress = data.data.Person.Addresses.Address.PostalAddress;
                $scope.PostalCode = data.data.Person.Addresses.Address.PostalCode;
                $scope.PostalPlace = data.data.Person.Addresses.Address.PostalPlace;
                $scope.sok = "";
                $scope.display = false;
                $scope.hideErr = true;
            }, function (err) {
                $scope.title = "SOMTHING WENT WRONG";
                $scope.display = true;
                $scope.err = err;
                //$scope.hideErr = false;
            });
        };
        
    });