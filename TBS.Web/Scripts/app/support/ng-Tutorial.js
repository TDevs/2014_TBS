var TutorialListController = function ($scope, $http, $modal,$sce, RoleService) {
    $scope.lstTutorials = new Array();
    LoadTutorials($http, $scope, $sce);

    $scope.AddTutorial = function () {
        var tutorial = {
            'Title': '',
            'Content': ''
        };
        var modalInstance = $modal.open({
            templateUrl: '/Support/CreateTutorial',
            controller: 'TutorialCreateController',
            resolve: {
                tutorial: function () {
                    return tutorial;
                }
            }
        });

        //reload list
        modalInstance.result.then(function () {
            LoadTutorials($http, $scope, $sce);

        });
    };

    RoleService.GetCurrentRole($http, $scope);

    $scope.EditTutorial = function (tutor) {
        var tutorial = null;
        // Get orginal before decode to HTML raw
        var count = 0;
        angular.forEach($scope.lstTutorials, function (value, key) {

            if (count == 0) {
                if (value.Id == tutor.Id) {
                    tutorial = value;
                    count++;
                }
            }
        })

        var modalInstance = $modal.open({
            templateUrl: '/Support/EditTutorial',
            controller: 'TutorialEditController',
            resolve: {
                tutorial: function () {
                    return angular.copy(tutorial);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            LoadTutorials($http, $scope, $sce);
        });
    };

    $scope.DeleteTutorial = function (tutorial) {
        if (confirm("Are you sure you want to delete this Tutorial?")) {
            $http({
                method: 'POST',
                url: '/Support/DeleteTutorial',
                data: tutorial,
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.Tutorials = jQuery.grep($scope.Tutorials, function (value) {
                    return value.Id != tutorial.Id;
                });
            })
        }
    };


};


var TutorialCreateController = function ($scope, $http, $modalInstance, tutorial) {

    $scope.FormTitle = "Create a new Tutorial";

    $scope.tutorial = tutorial;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {

            //save user
            $http({
                method: 'POST',
                url: '/Support/CreateTutorial',
                data: $scope.tutorial,
                async: false
            }).success(function (data, status, headers, config) {
                $modalInstance.close();
            });

        }
    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

var TutorialEditController = function ($scope, $http, $modalInstance, tutorial) {

    $scope.FormTitle = "Edit Tutorial";

    $scope.tutorial = tutorial;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {
            //save Edit Tutorial
            $http({
                method: 'POST',
                url: '/Support/EditTutorial',
                data: $scope.tutorial,
                async: true
            });
            $modalInstance.close();
        }


    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};



function LoadTutorials($http, $scope, $sce) {
    $scope.Tutorials = new Array();
    //save user
    $http({
        method: 'GET',
        url: '/Support/GetTutorials',
        async: false
    }).success(function (data, status, headers, config) {
        //$scope.Tutorials = data;
        angular.forEach(data, function (value, key) {
            $scope.lstTutorials.push(angular.copy(value));
            value.Content = $sce.trustAsHtml(value.Content);
            $scope.Tutorials.push(value);
        })
    });

}


var TutorialsController = function ($scope, $http, $sce) {
    $scope.lstTutorials = new Array();
    LoadTutorials($http, $scope,$sce);
};
